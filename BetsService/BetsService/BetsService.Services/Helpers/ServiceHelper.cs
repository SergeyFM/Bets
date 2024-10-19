using Bets.Abstractions.DataAccess.EF.Repositories;
using BetsService.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace BetsService.Services.Helpers
{
    public static class ServiceHelper
    {
        public static IServiceCollection AddBetsServices(this IServiceCollection services)
        {
            services
                .AddAutoMapper(typeof(MappingProfile))

                .AddScoped<LaterDeletedEntityRepository<EventOutcomes>>()
                .AddScoped<LaterDeletedEntityRepository<Events>>();

            return services;
        }
    }
}
