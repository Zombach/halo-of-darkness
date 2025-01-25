using System.Globalization;
using HaloOfDarkness.Server.Configuration;

namespace HaloOfDarkness.Server.Extensions.Configuration;

internal static class ConfigurationConfigure
{
    public static ConfigurationManager AddConfiguration(
        this ConfigurationManager configuration,
        string environment)
    {
        const string pathDefault = "appsettings.json";

        var directory = Path.GetDirectoryName(typeof(Program).Assembly.Location) ?? string.Empty;
        var basePath = Path.Combine(directory, "Configs");
        configuration.SetBasePath(basePath);

        var appSettingsPath = environment.ToLower(CultureInfo.CurrentCulture) switch
        {
            "development" => "appsettings.development.json",
            //"docker" => "appsettings.docker.json",
            _ => pathDefault,
        };

        configuration.AddAppSetting(pathDefault);

        if (!appSettingsPath.Equals(pathDefault))
        {
            configuration.AddAppSetting(appSettingsPath, optional: true);
        }

        return configuration;
    }
}
