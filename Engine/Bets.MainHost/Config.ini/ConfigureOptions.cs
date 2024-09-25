using Microsoft.Extensions.DependencyInjection;

namespace Bets.MainHost.Config.ini;

public static class ConfigureOptions {
    public static IServiceCollection AddConfOptions(this IServiceCollection services) {

        /*        
                services.AddOptions<NotificationServiceOption>()
                    .BindConfiguration("NotificationService")
                    .ValidateDataAnnotations()
                    .ValidateOnStart();                 */

        return services;
    }
}