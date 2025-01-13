using System;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using HaloOfDarkness.Shared.Grpc.Protos;

namespace HaloOfDarkness.Server.Infrastructure.GrpcServices;

public class TestService : TestGrpcService.TestGrpcServiceBase, ITestService
{
    public override async Task<Response> TestConnected(Request request, ServerCallContext context)
    {
        Console.WriteLine(request.Name);
        await Task.Delay(1000);
        return new Response { Message = string.Join("", request.Name.Reverse()) };
    }
}
