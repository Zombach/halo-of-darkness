using Grpc.Net.Client;
using HaloOfDarkness.Shared.Grpc.Protos.User.Identification;

namespace HaloOfDarkness.Client.Console;

internal sealed class GrpcProvider : IDisposable
{
    private readonly GrpcChannel _grpcChannel;
    private readonly IdentificationGrpcService.IdentificationGrpcServiceClient _clientIdentificationGrpcServiceClient;

    public GrpcProvider(/*IOptions<GrpcOptions> options, IConfiguration config*/)
    {
        _grpcChannel = GrpcChannel.ForAddress("http://localhost:5001", new GrpcChannelOptions { UnsafeUseInsecureChannelCallCredentials = true });
        _clientIdentificationGrpcServiceClient = new IdentificationGrpcService.IdentificationGrpcServiceClient(_grpcChannel);
    }

    public async Task<Response> Registration(string message)
    {
        var response = await _clientIdentificationGrpcServiceClient.RegistrationAsync(new Request { Name = message });

        return response;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            _grpcChannel.ShutdownAsync().Wait();
        }
    }
}
