// Extensions/ServiceCollectionExtensions.cs
using System;
using Microsoft.Extensions.DependencyInjection;

namespace Bets.MainHost.Config.ini;
public static class ServiceCollectionExtensions {
    public static IServiceCollection AddServices(this IServiceCollection services) {
        Console.WriteLine("Adding services...");
        return services;
    }
}
