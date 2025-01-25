using HaloOfDarkness.Server.Configuration;
using HaloOfDarkness.Server.Extensions;
using HaloOfDarkness.Server.Extensions.Configuration;

Serilog.ILogger? logger = null;
try
{
    EncodingConfigure.AddEncoding();

    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
                      ?? "development";

    var builder = WebApplication.CreateBuilder(args);
    builder.Configuration.AddConfigurations(environment);
    builder.BuilderConfigure(out var defaultLogger);

    logger = defaultLogger.ForContext<Program>();
    logger.Information("environment: {@environment}", environment);

    builder.Services.AddServices(builder.Configuration);

    var app = builder.Build();
    app.UseApplication();
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
