using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserServer.Core.Entities;
using UserServer.DataAccess.Data;

namespace UserServer.DataAccess.Extensions
{
    /// <summary>
    /// Расширение для подключения контекста и Кэша
    /// </summary>
    public static class DataExtension
    {
        /// <summary>
        /// Дабовления контеста в сервайс
        /// </summary>
        /// <param name="services">Контейнер зависимость</param>
        /// <param name="configuration">Конфигурация</param>
        /// <returns></returns>
        public static IServiceCollection AddDataSetvices(this IServiceCollection services, IConfiguration configuration)
        {
            string? connectionDb = configuration.GetConnectionString("DefaultConnection");
            if (String.IsNullOrEmpty(connectionDb)) { throw new NullReferenceException(nameof(connectionDb)); }
            connectionDb = string.Format(connectionDb,
                Environment.GetEnvironmentVariable("ASPNETCORE_DBBASE"),
                Environment.GetEnvironmentVariable("ASPNETCORE_DBUSER"),
                Environment.GetEnvironmentVariable("ASPNETCORE_DBPASSWORD"));


            services.AddDbContext<ApplicationDbContext>(option =>
                option.UseNpgsql(connectionDb));

            string? conntectionResdis = configuration.GetConnectionString("Redis");
            if (String.IsNullOrEmpty(connectionDb)) { throw new NullReferenceException(nameof(connectionDb)); }
            conntectionResdis = string.Format(conntectionResdis,
                Environment.GetEnvironmentVariable("ASPNETCORE_REDISPORT"),
                Environment.GetEnvironmentVariable("ASPNETCORE_REDIS_PASSWORD"));

            services.AddStackExchangeRedisCache(option =>
            {
                option.Configuration = conntectionResdis;
                option.InstanceName = "UserServer";
            });
            return services;
        }

        /// <summary>
        /// Выполнение миграций и заполнение первоначальных данных
        /// </summary>
        /// <param name="app">контейнер зависимотей</param>
        public static async Task AddMigrationAndSeedDataAsync(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var service = scope.ServiceProvider;
                try
                {
                    var context = service.GetService<ApplicationDbContext>();
                    await context.Database.MigrateAsync();
                    await SeedDatabBase(service, context);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while migrating the database: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// ЗАполнение первоначавльных данных в ДБ
        /// </summary>
        /// <param name="services">Провайдер</param>
        /// <param name="context">контекст</param>
        private static async Task SeedDatabBase(IServiceProvider services, ApplicationDbContext context)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<UserServer.Core.Entities.Role>>();

            // Пример загрузки ролей
            if (!(await roleManager.Roles.AnyAsync()))
            {
                var roles = new List<UserServer.Core.Entities.Role>
                {
                    new UserServer.Core.Entities.Role { Name = "Admin" },
                    new UserServer.Core.Entities.Role { Name = "User" },
                    new UserServer.Core.Entities.Role { Name = "Moderator" }
                };

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(role);
                }
            }

            // Пример загрузки пользователей
            if (!(await userManager.Users.AnyAsync()))
            {
                var adminUser = new User
                {
                    Email = "admin@example.com",
                    UserName = "admin@example.com",
                    FirstName = "Admin",
                    LastName = "Main"
                };

                var result = await userManager.CreateAsync(adminUser, "Qwerty202$");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
                else
                {
                    Console.WriteLine($"Error creating user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }

        }
    }
}
