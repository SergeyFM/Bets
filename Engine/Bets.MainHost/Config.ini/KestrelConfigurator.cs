using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace Bets.MainHost.Config.ini;

public static class KestrelConfigurator {
    public static WebApplicationBuilder ConfigureKestrelOptions(this WebApplicationBuilder builder) {
        builder.WebHost.ConfigureKestrel(options => {
            options.ListenAnyIP(8080, listenOptions => {
                listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
            });
        });

        return builder;
    }
}
