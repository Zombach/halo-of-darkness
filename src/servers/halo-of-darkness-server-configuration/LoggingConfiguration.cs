using System.Globalization;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Settings.Configuration;
using ILogger = Serilog.ILogger;

namespace HaloOfDarkness.Libs.Configuration;

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
        var logger = new LoggerConfiguration()
            .WriteTo.Console(
                LogEventLevel.Debug,
                formatProvider: new CultureInfo(CultureInfo.CurrentCulture.Name))
            .CreateBootstrapLogger();

        return logger;
    }
}
