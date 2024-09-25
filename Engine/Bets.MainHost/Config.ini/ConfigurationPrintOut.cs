using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Bets.MainHost.Config.ini;

public static class ConfigurationPrintOut {
    /// <summary>
    /// Prints all key-value pairs in the IConfiguration instance.
    /// </summary>
    /// <param name="configuration">The IConfiguration instance.</param>
    public static void PrintAllConfigurations(this IConfiguration configuration) {
        // Recursively traverse the configuration and print all key-value pairs
        foreach (KeyValuePair<string, string> kvp in GetAllConfigurationSettings(configuration)) Console.WriteLine($"{kvp.Key}: {kvp.Value}");
    }

    /// <summary>
    /// Recursively retrieves all key-value pairs from the IConfiguration instance.
    /// </summary>
    /// <param name="configuration">The IConfiguration instance.</param>
    /// <returns>A dictionary of all key-value pairs.</returns>
    private static IDictionary<string, string> GetAllConfigurationSettings(IConfiguration configuration) {
        Dictionary<string, string> data = [];
        foreach (IConfigurationSection section in configuration.GetChildren()) if (!section.GetChildren().Any()) data[section.Path] = section.Value ?? "";
            else {
                IDictionary<string, string> nestedData = GetAllConfigurationSettings(section);
                foreach (KeyValuePair<string, string> kvp in nestedData) data[kvp.Key] = kvp.Value;
            }
        return data;
    }
}
