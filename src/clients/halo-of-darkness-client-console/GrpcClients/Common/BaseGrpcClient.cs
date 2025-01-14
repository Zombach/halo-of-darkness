using Grpc.Core;
using Grpc.Net.Client;

namespace HaloOfDarkness.Client.Console.GrpcClients.Common;

internal abstract class BaseGrpcClient<T>
    : IDisposable
    where T : ClientBase<T>
{
    private readonly GrpcChannel _channel;
    protected readonly T Client;

    protected BaseGrpcClient(Func<(GrpcChannel Channel, T Client)> func)
    {
        var (channel, client) = func();
        _channel = channel;
        Client = client;
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
            _channel.ShutdownAsync().Wait();
        }
    }
}
