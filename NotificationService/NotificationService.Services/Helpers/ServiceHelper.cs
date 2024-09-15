﻿using Microsoft.Extensions.DependencyInjection;
using Bets.Abstractions.DataAccess.EF.Repositories;
using NotificationService.Domain;
using NotificationService.Domain.Directories;
using NotificationService.DataAccess.Repositories;

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
                .AddScoped<LaterDeletedEntityRepository<Bettors>>()
                .AddScoped<LaterDeletedEntityRepository<MessageSources>>()
                .AddScoped<BettorAddressRepository>();

            return services;
        }
    }
}
