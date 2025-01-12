using System.Globalization;
using System.Text;
using HaloOfDarkness.Server.Configuration;
using Serilog;
using Serilog.Events;
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

    var configuration = builder.Configuration;
    configuration.AddConfiguration(environment);

    var loggingBuilder = builder.Logging;
    loggingBuilder.ClearProviders();
    logger = loggingBuilder.AddLoggers(configuration, "LogDefault")
        .ForContext<Program>();

    logger.Information("environment: {@environment}", environment);

    builder.Services.AddSwaggerGen();
    builder.Services.AddControllers();

    var app = builder.Build();
    app.MapControllers();
    app.UseSwagger();
    app.UseSwaggerUI();
    await app.RunAsync();
}
catch (Exception exception)
{
    logger ??= new LoggerConfiguration()
        .WriteTo.Console(
            LogEventLevel.Debug,
            formatProvider: new CultureInfo(CultureInfo.CurrentCulture.Name))
        .CreateLogger()
        .ForContext<Program>();

    logger.Fatal(
        exception,
        "message: {@message}",
        exception.Message);
}
finally
{
    logger?.Information("finish");
}
