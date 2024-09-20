using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace NotificationService.MailServices.Helper
{
    public static class ServiceHelper
    {
        public static IServiceCollection AddMailServices(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<MailSettings>(config.GetSection(nameof(MailSettings)));

            return services;
        }
    }
}
