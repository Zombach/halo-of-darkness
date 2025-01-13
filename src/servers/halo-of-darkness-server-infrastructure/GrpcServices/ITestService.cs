using System.Threading.Tasks;
using Grpc.Core;
using HaloOfDarkness.Shared.Grpc.Protos;

namespace HaloOfDarkness.Server.Infrastructure.GrpcServices;

public interface ITestService
{
    Task<Response> TestConnected(Request request, ServerCallContext context);
}
