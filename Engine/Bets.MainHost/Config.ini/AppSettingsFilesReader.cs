
// Extensions/ConfigurationBuilderExtensions.cs
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Bets.MainHost.Config.ini;

public static class ConfigurationBuilderExtensions {
    public static IConfiguration ReadAppSettings(this WebApplicationBuilder builder, string folderPath = "Config.ini") {

        // Retrieve the environment name from the builder
        string environmentName = builder.Environment.EnvironmentName;

        // Clear the default configuration providers
        builder.Configuration.Sources.Clear();

        // Add configuration files from the specified folder
        builder.Configuration
            //.AddJsonFile(Path.Combine(folderPath, "appsettings.json"), optional: true, reloadOnChange: true)
            .AddJsonFile(Path.Combine(folderPath, $"appsettings.{environmentName}.json"), optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

        return builder.Configuration;
    }
}

