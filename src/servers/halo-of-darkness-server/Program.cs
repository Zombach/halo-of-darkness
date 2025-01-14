using System.Net;
using System.Text;
using HaloOfDarkness.Server.Configuration;
using HaloOfDarkness.Server.Infrastructure;
using Microsoft.AspNetCore.Server.Kestrel.Core;
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

    builder.Configuration.AddConfiguration(environment);

    builder.AddLoggers(out var defaultLogger, "LogDefault");
    logger = defaultLogger.ForContext<Program>();

    logger.Information("environment: {@environment}", environment);

    var services = builder.Services;

    services.AddOptions(builder.Configuration);
    services.AddMiddlewares();
    services.AddRouting(options => options.LowercaseUrls = true);

    services.AddSwaggerGen();
    services.AddControllers();

    services.AddGrpc();
    services.AddInfrastructure(builder.Configuration);

    var app = builder.Build();
    app.UseMiddleware();

    app.MapControllers();
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseInfrastructure();
    app.UseRouting();
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
