using System.Net;
using System.Reflection;
using System.Text;
using HaloOfDarkness.Server.Configuration;
using HaloOfDarkness.Server.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.OpenApi.Models;
using ILogger = Serilog.ILogger;

ILogger? logger = default;
try
{
    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
    Console.OutputEncoding = Encoding.UTF8;
    Console.InputEncoding = Encoding.UTF8;

    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
                      ?? "development";

    var builder = WebApplication.CreateBuilder(args);
    var services = builder.Services;
    var configuration = builder.Configuration;
    var host = builder.Host;

    builder.WebHost.ConfigureKestrel(options =>
    {
        var httpPort = 5000;
        var grpcPort = 5001;

        options.Listen(
            IPAddress.Any,
            httpPort,
            listenOptions => listenOptions.Protocols = HttpProtocols.Http1);

        options.Listen(
            IPAddress.Any,
            grpcPort,
            listenOptions => listenOptions.Protocols = HttpProtocols.Http2);
    });

    configuration.AddConfiguration(environment);

    builder.AddLoggers(out var defaultLogger, "LogDefault");
    logger = defaultLogger.ForContext<Program>();

    logger.Information("environment: {@environment}", environment);

    services.AddOptions(configuration);

    services.AddHttpContextAccessor();
    services.AddMiddlewares();

    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddRouting(options => options.LowercaseUrls = true);

    services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

        options.AddSecurityDefinition("Keycloak", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.OAuth2,
            Flows = new OpenApiOAuthFlows
            {
                Implicit = new OpenApiOAuthFlow
                {
                    AuthorizationUrl = new Uri("http://localhost:8080/realms/halo_of_darkness/protocol/openid-connect/auth"),
                    Scopes = new Dictionary<string, string>
                    {
                        { "openid", "openid" },
                        { "profile", "profile" }
                    }
                }
            }
        });

        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.ApiKey,
            Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
            Name = "Authorization",
            In = ParameterLocation.Header
        });

        var keycloakSecurityScheme = new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Id = "Keycloak",
                Type = ReferenceType.SecurityScheme
            },
            In = ParameterLocation.Header,
            Name = "Bearer",
            Scheme = "Bearer"
        };

        var keycloakSecuritySchemeToken = new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Id = "Bearer",
                Type = ReferenceType.SecurityScheme
            },
            In = ParameterLocation.Header,
            Name = "Bearer",
            Scheme = "Bearer"
        };

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            { keycloakSecurityScheme, Array.Empty<string>() },
            { keycloakSecuritySchemeToken, Array.Empty<string>() },
        });

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);
    });

    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, o =>
        {
            o.MetadataAddress = "http://localhost:8080/realms/halo_of_darkness/.well-known/openid-configuration";
            o.Authority = "http://localhost:8080/realms/halo_of_darkness";
            o.Audience = "halo-of-darkness-server";
            o.RequireHttpsMetadata = false;
        });

    services.AddGrpc();
    services.AddInfrastructure(builder.Configuration);

    var app = builder.Build();
    app.UseMiddleware();

    app.UseInfrastructure();

    app.UseRouting();

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        options.OAuthClientId("halo-of-darkness-server");
        options.OAuthClientSecret("WtETGjNxO0tB8q9pl3XVjf3CB1SBexwd");
    });

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
    //app.UseEndpoints(options => options.MapControllers());
    await app.RunAsync();
}
catch (Exception exception)
{
    logger ??= LoggingConfiguration
        .CreateDefaultLogger()
        .ForContext<Program>();

    logger.Fatal(
        exception,
        "message: {@message}",
        exception.Message);
}
finally
{
    logger!.Information("finish");
}
