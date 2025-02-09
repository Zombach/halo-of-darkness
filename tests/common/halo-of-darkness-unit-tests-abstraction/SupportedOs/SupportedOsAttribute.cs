using System.Reflection;
using System.Runtime.InteropServices;
using Xunit.v3;

namespace HaloOfDarkness.UnitTests.Abstraction.SupportedOs;

public sealed class SupportedOsAttribute(params SupportedOses[] supportedOses) :
    BeforeAfterTestAttribute
{
    private static readonly Dictionary<SupportedOses, OSPlatform> s_osMappings = new()
    {
        { SupportedOses.FreeBSD, OSPlatform.Create("FreeBSD") },
        { SupportedOses.Linux, OSPlatform.Linux },
        { SupportedOses.macOS, OSPlatform.OSX },
        { SupportedOses.Windows, OSPlatform.Windows },
    };

    public override void Before(MethodInfo methodUnderTest, IXunitTest test)
    {
        var match = false;

        foreach (var supportedOs in supportedOses)
        {
            if (!s_osMappings.TryGetValue(supportedOs, out var osPlatform))
            {
                throw new ArgumentException($"Supported OS value '{supportedOs}' is not a known OS", nameof(supportedOses));
            }

            if (RuntimeInformation.IsOSPlatform(osPlatform))
            {
                match = true;
                break;
            }
        }

        // We use the dynamic skip exception message pattern to turn this into a skipped test
        // when it's not running on one of the targeted OSes
        if (!match)
        {
            throw new Exception($"$XunitDynamicSkip$This test is not supported on {RuntimeInformation.OSDescription}");
        }
    }
}
