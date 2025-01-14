using Grpc.Net.Client;
using HaloOfDarkness.Client.Console.GrpcClients.Common;
using HaloOfDarkness.Shared.Grpc.Protos.User.Authentication;

namespace HaloOfDarkness.Client.Console.GrpcClients;

internal sealed class AuthenticationClient(Uri url)
    : BaseGrpcClient<AuthenticationGrpcService.AuthenticationGrpcServiceClient>(() =>
    {
        var channel = GrpcChannel.ForAddress(url);
        var client = new AuthenticationGrpcService.AuthenticationGrpcServiceClient(channel);

        return (channel, client);
    })
{
    public async Task<Response> LogIn(string message)
    {
        var response = await Client.LogInAsync(new Request { Name = message });

        return response;
    }
}
