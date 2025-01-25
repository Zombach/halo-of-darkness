using HaloOfDarkness.Server.Configuration;
using HaloOfDarkness.Server.Options;

namespace HaloOfDarkness.Server.Extensions.Configuration;

internal static class OptionsConfigure
{
    public static IServiceCollection AddOptions(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var requestProfilingSection = configuration.GetSection(RequestProfilingOptions.SectionKey);
        services.AddOptionsWithValidate<RequestProfilingOptions>(requestProfilingSection);

        return services;
    }
}
