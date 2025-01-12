using Microsoft.Extensions.Configuration;

namespace HaloOfDarkness.Libs.Configuration;

public static class ConfigurationConfigure
{
    public static IConfigurationBuilder AddAppSetting(
        this IConfigurationBuilder configuration,
        string appSettingsPath,
        bool optional = false,
        bool reloadOnChange = false)
    {
        configuration.AddJsonFile
        (
            appSettingsPath,
            optional: optional,
            reloadOnChange: reloadOnChange
        );

        return configuration;
    }
}
