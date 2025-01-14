using HaloOfDarkness.Server.Infrastructure.Grpc;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HaloOfDarkness.Server.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGrpcServers(configuration);

        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication application)
    {
        application.UseGrpcServers();

        return application;
    }
}
