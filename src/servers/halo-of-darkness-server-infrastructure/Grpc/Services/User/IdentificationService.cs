using System;
using System.Threading.Tasks;
using Grpc.Core;
using HaloOfDarkness.Server.Grpc.Services.Common.User;
using HaloOfDarkness.Shared.Grpc.Protos.User.Identification;

namespace HaloOfDarkness.Server.Infrastructure.Grpc.Services.User;

internal sealed class IdentificationService
    : IdentificationGrpcService.IdentificationGrpcServiceBase, IIdentificationService
{
    public override Task<Response> Registration(Request request, ServerCallContext context)
    {
        return Task.FromResult(new Response { Message = $"{nameof(Registration)}: {DateTime.Now}" });
    }

    public override Task<Response> RecoverPassword(Request request, ServerCallContext context)
    {
        return Task.FromResult(new Response { Message = $"{nameof(RecoverPassword)}: {DateTime.Now}" });
    }
}
