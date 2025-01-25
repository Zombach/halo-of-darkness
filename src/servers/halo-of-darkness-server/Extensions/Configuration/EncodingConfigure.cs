using System.Text;

namespace HaloOfDarkness.Server.Extensions.Configuration;

internal static class EncodingConfigure
{
    public static void AddEncoding()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
    }
}
