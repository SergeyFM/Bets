using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.HttpLogging;
using Serilog;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace UserServer.DataAccess.Extensions
{
    /// <summary>
    /// Класс расширения для логирования
    /// </summary>
    public static class LoggerExtensions
    {
        /// <summary>
        /// Добавление логира в сервисы
        /// </summary>
        /// <param name="services">Контейнер сервисов</param>
        /// <param name="configuration">Конфигурация приложения</param>
        /// <returns></returns>
        public static WebApplicationBuilder AddLogger(this WebApplicationBuilder builder, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("app_name", "UserServer")
                .CreateLogger();

            builder.Host.UseSerilog();

            builder.Services.AddHttpLogging(logging =>
            {
                logging.LoggingFields = HttpLoggingFields.Request | HttpLoggingFields.Response | HttpLoggingFields.RequestBody | HttpLoggingFields.ResponseBody;
            });

            return builder;
        }

        /// <summary>
        /// Подключение middleware логирования
        /// </summary>
        /// <typeparam name="T">Подключаемый тип</typeparam>
        /// <param name="app">Экземпляр приложения</param>
        /// <returns>экземпляр приложения</returns>
        public static WebApplication AddHttpLogging<T>(this WebApplication app)
        {
            app.UseHttpLogging();

            // Пример использования логирования
            app.UseSerilogRequestLogging();

            var log = app.Services.GetService<ILogger<T>>();
            log?.LogDebug("debug");
            log?.LogInformation("info");

            return app;
        }
    }
}
