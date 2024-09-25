using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Bets.MainHost.Config.ini;

public static class LoggerInit {
    /// <summary>
    /// Configures Serilog for the application.
    /// </summary>
    /// <param name="builder">The WebApplicationBuilder instance.</param>
    /// <param name="configuration">Optional IConfiguration instance. If not provided, uses the configuration from WebApplicationBuilder.</param>
    public static void AddLogService(this WebApplicationBuilder builder, IConfiguration? configuration = null) {
        // Use the builder's configuration if none is provided
        configuration ??= builder.Configuration;

        // Serilog
#if DEBUG
        Serilog.Debugging.SelfLog.Enable(Console.Out);
#endif
        Log.Logger = new LoggerConfiguration()
            .Enrich.WithProperty("Username", Environment.UserName)
            .Enrich.WithProperty("MachineName", Environment.MachineName)
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        builder.Host.UseSerilog();
    }
}
