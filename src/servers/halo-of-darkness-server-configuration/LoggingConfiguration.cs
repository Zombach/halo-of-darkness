using System.Globalization;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Settings.Configuration;
using ILogger = Serilog.ILogger;

namespace HaloOfDarkness.Server.Configuration;

public static class LoggingConfiguration
{
    public static ILogger CreateLogger(
        IConfiguration configuration,
        string sectionKey)
    {
        var logger = new LoggerConfiguration().ReadFrom
            .Configuration(
                configuration,
                new ConfigurationReaderOptions { SectionName = sectionKey })
            .Enrich.FromLogContext()
            .CreateBootstrapLogger();

        return logger;
    }

    public static ILogger CreateDefaultLogger()
    {
        const string template = "[{Level:u4}] |{SourceContext,30}({EventId})| {Message:lj}{NewLine}{Exception}";

        var logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.Console(
                restrictedToMinimumLevel: LogEventLevel.Debug,
                outputTemplate: template,
                formatProvider: new CultureInfo(CultureInfo.CurrentCulture.Name))
            .CreateBootstrapLogger();

        return logger;
    }
}
