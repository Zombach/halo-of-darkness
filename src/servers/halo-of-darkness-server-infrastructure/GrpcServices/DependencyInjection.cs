using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HaloOfDarkness.Server.Infrastructure.GrpcServices;

public static class DependencyInjection
{
    public static IServiceCollection AddGrpcServer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ITestService, TestService>();

        return services;
    }
}
