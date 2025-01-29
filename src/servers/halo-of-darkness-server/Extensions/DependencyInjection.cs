using HaloOfDarkness.Server.Application;
using HaloOfDarkness.Server.Extensions.Configuration;
using HaloOfDarkness.Server.Infrastructure;

namespace HaloOfDarkness.Server.Extensions;

internal static class DependencyInjection
{
    public static WebApplicationBuilder BuilderConfigure(
        this WebApplicationBuilder builder,
        out Serilog.ILogger defaultLogger)
    {
        builder.ConfigureKestrel();
        builder.AddLoggers(out defaultLogger, "LogDefault");
        return builder;
    }

    public static IConfiguration AddConfigurations(
        this ConfigurationManager configuration,
        string environment)
    {
        configuration.AddConfiguration(environment);
        return configuration;
    }

    public static IServiceCollection AddServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOptions(configuration);

        services.AddHttpContextAccessor();
        services.AddMiddlewares();

        services.AddControllers();

        services.AddEndpointsApiExplorer();
        services.AddRouting(options => options.LowercaseUrls = true);

        services.AddSwagger();

        // adds client resource claims transformation
        //services.AddKeycloakWebApiAuthentication(
        //    configuration,
        //    bearerOptions =>
        //    {
        //        bearerOptions.TokenValidationParameters = new()
        //        {
        //            ValidAudiences = ["halo-of-darkness-server", "account"],
        //            ValidateIssuerSigningKey = false,
        //            ValidIssuer = "http://localhost:8080/realms/halo_of_darkness",
        //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("y24skgZMPJvDkVh6r7afNVQKNRfUrwb7"))
        //        };
        //        bearerOptions.MetadataAddress = "http://keycloak:8080/realms/halo_of_darkness/.well-known/openid-configuration";
        //        bearerOptions.RequireHttpsMetadata = false;
        //        bearerOptions.Audience = "test-client";
        //    });

        //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        //    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, o =>
        //    {
        //        o.MetadataAddress = "http://localhost:8080/realms/halo_of_darkness/.well-known/openid-configuration";
        //        o.Authority = "http://localhost:8080/realms/halo_of_darkness";
        //        o.Audience = "halo-of-darkness-server";
        //        o.RequireHttpsMetadata = false;
        //    });

        services.AddGrpc();
        services.AddApplication();
        services.AddInfrastructure(configuration);

        return services;
    }

    public static WebApplication UseApplication(this WebApplication application)
    {
        application.UseMiddlewares();

        application.UseInfrastructure();

        application.UseRouting();

        application.UseSwagger();
        application.UseSwaggerUI(options =>
        {
            //options.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
            //options.OAuthClientId("halo-of-darkness-server");
            //options.OAuthClientSecret("WtETGjNxO0tB8q9pl3XVjf3CB1SBexwd");
        });

        application.UseAuthentication();
        application.UseAuthorization();

        application.MapControllers();
        //application.UseEndpoints(options => options.MapControllers());

        return application;
    }
}
