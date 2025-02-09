using DotNet.Testcontainers.Containers;

namespace HaloOfDarkness.FunctionalTests.Abstraction.Docker;

public sealed class DockerContainer(IContainer container, int port)
{
    public int Port => port;
    public IContainer Container => container;
}
