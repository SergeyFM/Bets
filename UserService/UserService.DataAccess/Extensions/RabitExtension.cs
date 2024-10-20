using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserServer.Core.DTO;

namespace UserServer.DataAccess.Extensions
{
    /// <summary>
    /// Класс расширений рабит
    /// </summary>
    public static class RabitExtension
    {
        /// <summary>
        /// Подключение Rabit
        /// </summary>
        /// <param name="services">Коллекция сервисов</param>
        /// <param name="configuration">Конфигурация приложений</param>
        /// <returns></returns>
        public static IServiceCollection AddRabitSetvices(this IServiceCollection services, IConfiguration configuration)
        {
            var rabitConfig = configuration.GetSection("Rabit");

            var rabitPortStr = Environment.GetEnvironmentVariable("ASPNETCORE_RABITPROT");
            if (string.IsNullOrEmpty(rabitPortStr)) 
            {
                throw new ArgumentNullException(nameof(rabitPortStr), "Не найдена конфигурация RabbitMQ");
            }

            ushort rabitPort = 0;
            if (!ushort.TryParse(rabitPortStr, out rabitPort))
                throw new ArgumentException("Не удалось преобразовать порт в число");

            var rabitUser = Environment.GetEnvironmentVariable("ASPNETCORE_RABIT_USER");
            var rabitPassword = Environment.GetEnvironmentVariable("ASPNETCORE_RABIT_PASSWORD");

            if (string.IsNullOrEmpty(rabitPassword) || string.IsNullOrEmpty(rabitUser))
                throw new ArgumentNullException("Не найден логин или пароль");

            services.AddMassTransit(x =>
            {
                x.AddConsumer<UserCreatedConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(rabitConfig["HostName"], rabitPort, rabitConfig["VHost"], (h) =>
                    {
                        h.Username(rabitUser); // Укажите имя пользователя
                        h.Password(rabitPassword); // Укажите пароль
                    });
                    cfg.ConfigureEndpoints(context);
                });
            });

            return services;
        }
    }

    /// <summary>
    /// для примера работы с паблишем юзеров оставил очень временно
    /// </summary>
    public class UserCreatedConsumer : IConsumer<ResponceUserDto>
    {
        public Task Consume(ConsumeContext<ResponceUserDto> context)
        {
            var message = context.Message;
            Console.WriteLine(message);
            return Task.CompletedTask;
        }
    }
}
