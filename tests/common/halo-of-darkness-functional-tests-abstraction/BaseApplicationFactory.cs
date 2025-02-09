using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HaloOfDarkness.FunctionalTests.Abstraction;

public abstract class BaseApplicationFactory
    : IAsyncLifetime
{
    private Lazy<TestServer>? _lazyServer;
    public TestServer Server => _lazyServer?.Value ?? throw new ArgumentNullException(nameof(Server));

    protected virtual string[]? Arguments => null;

    protected virtual HashSet<string> IgnoreModules => [];

    public virtual ValueTask InitializeAsync()
    {
        _lazyServer = new Lazy<TestServer>(CreateHostInternal, LazyThreadSafetyMode.ExecutionAndPublication);
        AfterInitialize();
        return ValueTask.CompletedTask;
    }

    protected virtual TestServer CreateHostInternal()
    {
        //var host = Bootstrap.StartAsync(
        //        builder =>
        //        {
        //            builder.UseTestServer();

        //            // Дополняем конфигурацию энвами для тестирования
        //            builder.UseEnvironment("testing");

        //            builder.ConfigureAppConfiguration(ConfigureAppConfiguration);

        //            builder.ConfigureServices((context, collection) => Bootstrap.ConfigureServices(context, collection));

        //            builder.ConfigureServices(services =>
        //            {
        //                ConfigureDatabase(services);
        //                ConfigureServices(services);
        //            });

        //            builder.Configure((context, application) => Bootstrap.ConfigureApp(context, application));
        //        },
        //        Arguments,
        //        IgnoreModules)
        //    .ConfigureAwait(false)
        //    .GetAwaiter()
        //    .GetResult();

        var host = Host.CreateDefaultBuilder()
            .Build();

        return host.GetTestServer();
    }

    protected virtual void ConfigureAppConfiguration(WebHostBuilderContext context, IConfigurationBuilder configurationBuilder)
    {
        var environment = context.HostingEnvironment;

        configurationBuilder.AddJsonFile("appsettings.json", false)
            .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", false);
    }

    protected virtual void ConfigureDatabase(IServiceCollection services) { }

    protected virtual void ConfigureServices(IServiceCollection services) { }

    protected virtual void AfterInitialize() { }

    public virtual ValueTask DisposeAsync()
    {
        if (_lazyServer?.IsValueCreated is true)
        {
            _lazyServer.Value.Dispose();
        }

        return ValueTask.CompletedTask;
    }
}
