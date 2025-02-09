using Microsoft.AspNetCore.TestHost;

namespace HaloOfDarkness.FunctionalTests.Abstraction;

public abstract class BaseControllerTests<TApplicationFactory>(TApplicationFactory applicationFactory)
    where TApplicationFactory : BaseApplicationFactory
{
    private readonly Lazy<TestServer>? _lazyServer = new(() => applicationFactory.Server, LazyThreadSafetyMode.ExecutionAndPublication);
    private readonly Lazy<IServiceProvider>? _lazyServices = new(() => applicationFactory.Server.Services, LazyThreadSafetyMode.ExecutionAndPublication);
    private readonly Lazy<HttpClient>? _lazyClient = new(applicationFactory.Server.CreateClient, LazyThreadSafetyMode.ExecutionAndPublication);

    protected TestServer Server => _lazyServer?.Value ?? throw new ArgumentNullException(nameof(Server));
    protected IServiceProvider Services => _lazyServices?.Value ?? throw new ArgumentNullException(nameof(Services));
    protected HttpClient Client => _lazyClient?.Value ?? throw new ArgumentNullException(nameof(Client));
}
