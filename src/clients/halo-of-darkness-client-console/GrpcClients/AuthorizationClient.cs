using Grpc.Net.Client;
using HaloOfDarkness.Client.Console.GrpcClients.Common;
using HaloOfDarkness.Shared.Grpc.Protos.User.Authorization;

namespace HaloOfDarkness.Client.Console.GrpcClients;

internal sealed class AuthorizationClient(Uri url)
    : BaseGrpcClient<AuthorizationGrpcService.AuthorizationGrpcServiceClient>(() =>
    {
        var channel = GrpcChannel.ForAddress(url);
        var client = new AuthorizationGrpcService.AuthorizationGrpcServiceClient(channel);

        return (channel, client);
    })
{
    public async Task<Response> TestConnected(string message)
    {
        var response = await Client.TestConnectedAsync(new Request { Name = message });

        return response;
    }
}
