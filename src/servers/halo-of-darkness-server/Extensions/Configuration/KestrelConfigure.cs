using System.Net;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace HaloOfDarkness.Server.Extensions.Configuration;

internal static class KestrelConfigure
{
    public static WebApplicationBuilder ConfigureKestrel(this WebApplicationBuilder builder)
    {
        builder.WebHost.ConfigureKestrel(options =>
        {
            //TODO: Get for IConfiguration
            var httpPort = 5000;
            options.Listen(httpPort, HttpProtocols.Http1AndHttp2AndHttp3);

            var grpcPort = 5001;
            options.Listen(grpcPort, HttpProtocols.Http2);
        });

        return builder;
    }

    private static KestrelServerOptions Listen(
        this KestrelServerOptions options,
        int port,
        HttpProtocols protocols)
    {
        options.Listen(
            IPAddress.Any,
            port,
            listenOptions => listenOptions.Protocols = protocols);

        return options;
    }
}
