using System.Text;
using HaloOfDarkness.Libs.Configuration;
using HaloOfDarkness.Server.Configuration;
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

    var app = builder.Build();
    app.UseMiddlewares();

    app.MapControllers();
    app.UseSwagger();
    app.UseSwaggerUI();
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
