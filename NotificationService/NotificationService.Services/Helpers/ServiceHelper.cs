using Microsoft.Extensions.DependencyInjection;
using Bets.Abstractions.DataAccess.EF.Repositories;
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
                .AddScoped<LaterDeletedEntityRepository<Messengers>>()
                .AddScoped<LaterDeletedEntityRepository<Bettors>>();

            return services;
        }
    }
}
