using Microsoft.Extensions.DependencyInjection;
using NotificationService.DataAccess.Abstractions.EF.Repositories;
using NotificationService.Domain;
using NotificationService.Domain.Directories;

namespace NotificationService.Services.Helpers
{
    public static class ServiceHelper
    {
        public static IServiceCollection AddNotificationServices(this IServiceCollection services)
        {
            services
                .AddAutoMapper(typeof(MappingProfile))

                .AddScoped<CreatedEntityRepository<IncomingMessages>>()
                .AddScoped<LaterDeletedEntityRepository<Messengers>>();

            return services;
        }
    }
}
