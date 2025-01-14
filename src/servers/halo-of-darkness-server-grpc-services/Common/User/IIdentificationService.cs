using Grpc.Core;
using HaloOfDarkness.Shared.Grpc.Protos.User.Identification;

namespace HaloOfDarkness.Server.Grpc.Services.Common.User;

public interface IIdentificationService
{
    Task<Response> Registration(Request request, ServerCallContext context);
    Task<Response> RecoverPassword(Request request, ServerCallContext context);
}
