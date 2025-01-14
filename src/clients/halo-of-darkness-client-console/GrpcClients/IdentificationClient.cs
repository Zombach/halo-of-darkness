using Grpc.Net.Client;
using HaloOfDarkness.Client.Console.GrpcClients.Common;
using HaloOfDarkness.Shared.Grpc.Protos.User.Identification;

namespace HaloOfDarkness.Client.Console.GrpcClients;

internal sealed class IdentificationClient(Uri url)
    : BaseGrpcClient<IdentificationGrpcService.IdentificationGrpcServiceClient>(() =>
{
    var channel = GrpcChannel.ForAddress(url);
    var client = new IdentificationGrpcService.IdentificationGrpcServiceClient(channel);

    return (channel, client);
})
{
    public async Task<Response> Registration(string message)
    {
        var response = await Client.RegistrationAsync(new Request { Name = message });

        return response;
    }
}
