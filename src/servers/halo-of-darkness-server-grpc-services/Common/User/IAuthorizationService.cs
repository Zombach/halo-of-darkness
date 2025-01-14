using Grpc.Core;
using HaloOfDarkness.Shared.Grpc.Protos.User.Authorization;

namespace HaloOfDarkness.Server.Grpc.Services.Common.User;

public interface IAuthorizationService
{
    Task<Response> TestConnected(Request request, ServerCallContext context);
}
