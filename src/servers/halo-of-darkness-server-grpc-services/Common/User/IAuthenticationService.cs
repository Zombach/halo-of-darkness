using Grpc.Core;
using HaloOfDarkness.Shared.Grpc.Protos.User.Authentication;

namespace HaloOfDarkness.Server.Grpc.Services.Common.User;

public interface IAuthenticationService
{
    Task<Response> LogIn(Request request, ServerCallContext context);
}
