using System.Threading.Tasks;
using Grpc.Core;
using HaloOfDarkness.Server.Grpc.Services.Common.User;
using HaloOfDarkness.Shared.Grpc.Protos.User.Authentication;

namespace HaloOfDarkness.Server.Infrastructure.Grpc.Services.User;

internal sealed class AuthenticationService
    : AuthenticationGrpcService.AuthenticationGrpcServiceBase, IAuthenticationService
{
    public override Task<Response> LogIn(Request request, ServerCallContext context)
    {
        return base.LogIn(request, context);
    }
}
