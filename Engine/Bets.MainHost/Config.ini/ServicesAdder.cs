// Extensions/ServiceCollectionExtensions.cs
using Microsoft.Extensions.DependencyInjection;

using System;

namespace Bets.MainHost.Config.ini; 
public static class ServiceCollectionExtensions {
    public static IServiceCollection AddServices(this IServiceCollection services) {
        Console.WriteLine("Adding services...");
        return services;
    }
}
