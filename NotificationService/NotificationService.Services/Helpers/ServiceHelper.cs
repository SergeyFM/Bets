using Microsoft.Extensions.DependencyInjection;
using NotificationService.DataAccess.Abstractions.EF.Repositories;
using NotificationService.Domain;

namespace NotificationService.Services.Helpers
{
    public static class ServiceHelper
    {
        public static IServiceCollection AddNotificationServices(this IServiceCollection services)
        {
            services
                .AddAutoMapper(typeof(MappingProfile))

                .AddScoped<CreatedEntityRepository<IncomingMessages>>();

            return services;
        }
    }
}
