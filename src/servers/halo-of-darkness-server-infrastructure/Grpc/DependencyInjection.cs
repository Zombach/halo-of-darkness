using HaloOfDarkness.Server.Grpc.Services.Common.User;
using HaloOfDarkness.Server.Infrastructure.Grpc.Services.User;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HaloOfDarkness.Server.Infrastructure.Grpc;

internal static class DependencyInjection
{
    public static IServiceCollection AddGrpcServers(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IAuthenticationService, AuthenticationService>();
        services.AddTransient<IAuthorizationService, AuthorizationService>();
        services.AddTransient<IIdentificationService, IdentificationService>();

        return services;
    }

    public static WebApplication UseGrpcServers(this WebApplication application)
    {
        application.MapGrpcService<AuthenticationService>();
        application.MapGrpcService<AuthorizationService>();
        application.MapGrpcService<IdentificationService>();

        return application;
    }
}
