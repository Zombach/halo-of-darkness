using System.Threading.Tasks;
using Grpc.Core;
using HaloOfDarkness.Server.Grpc.Services.Common.User;
using HaloOfDarkness.Shared.Grpc.Protos.User.Authorization;

namespace HaloOfDarkness.Server.Infrastructure.Grpc.Services.User;

internal sealed class AuthorizationService
    : AuthorizationGrpcService.AuthorizationGrpcServiceBase, IAuthorizationService
{
    public override Task<Response> TestConnected(Request request, ServerCallContext context)
    {
        return base.TestConnected(request, context);
    }
}
