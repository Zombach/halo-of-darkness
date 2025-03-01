using System.Net;
using System.Net.Sockets;

namespace HaloOfDarkness.FunctionalTests.Abstraction.Docker.Utilities;

public sealed class DockerUtilities
{
    public static int GetFreePort()
    {
        // From https://stackoverflow.com/a/150974/4190785
        var tcpListener = new TcpListener(IPAddress.Loopback, 0);
        tcpListener.Start();
        var port = ((IPEndPoint)tcpListener.LocalEndpoint).Port;
        tcpListener.Stop();
        return port;
    }
}
