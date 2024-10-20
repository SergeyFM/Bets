using Microsoft.EntityFrameworkCore;
using BetsService.Services;
using BetsService.DataAccess;

namespace BetsService.Api.Helpers
{
    public static class ServiceHelper
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
        {
            var connectionName = "betsDb";

            var connectionString = config.GetConnectionString(connectionName);
            if (string.IsNullOrEmpty(connectionString))
                throw new Exception($"Connection string {connectionName} not defined");

            //services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(connectionString));
            services.AddDbContextFactory<DatabaseContext>(options => options.UseNpgsql(string.Format(connectionString
                    , Environment.GetEnvironmentVariable("ASPNETCORE_DBBASE")
                    , Environment.GetEnvironmentVariable("ASPNETCORE_DBUSER")
                    , Environment.GetEnvironmentVariable("ASPNETCORE_DBPASSWORD"))));

            services
                .AddScoped<EventsService>()
                .AddScoped<EventOutcomesService>()
                .AddScoped<DbContext, DatabaseContext>();

            return services;
        }
    }
}
