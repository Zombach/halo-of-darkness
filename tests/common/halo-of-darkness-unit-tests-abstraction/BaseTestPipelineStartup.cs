using Xunit.Sdk;
using Xunit.v3;

namespace HaloOfDarkness.UnitTests.Abstraction;

public abstract class BaseTestPipelineStartup
    : ITestPipelineStartup
{
    public virtual ValueTask StartAsync(IMessageSink diagnosticMessageSink)
    {
        Console.WriteLine("Start");
        return new ValueTask();
    }

    public virtual ValueTask StopAsync()
    {
        Console.WriteLine("Finish");
        return new ValueTask();
    }
}
