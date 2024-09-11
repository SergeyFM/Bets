using Microsoft.EntityFrameworkCore;
using NotificationService.Services;
using NotificationService.DataAccess;

namespace NotificationService.Api.Helpers
{
    public static class ServiceHelper
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
        {
            var connectionName = "notificationDb";

            var connectionString = config.GetConnectionString(connectionName);
            if (string.IsNullOrEmpty(connectionString))
                throw new Exception($"Connection string {connectionName} not defined");

            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionString));
            services
                .AddScoped<IncomingMessagesService>()
                .AddScoped<MessengersService>()
                .AddScoped<BettorsService>()
                .AddScoped<DbContext, DatabaseContext>();

            return services;
        }
    }
}
