using UserServer.Core.Service;
using UserServer.WebHost.Middleware;

namespace UserServer.WebHost.Extensions
{
    /// <summary>
    /// класс расширения для подключения кастомных middleware
    /// </summary>
    public static class UseMiddlewareExtensiion
    {
        public static Task AddCustomMiddleware(this WebApplication app)
        {
            app.Use(async (context, next) =>
            {
                var jwtUtil = context.RequestServices.GetRequiredService<JwtUtils>();
                var middlevare = new JwtMiddleware(next, jwtUtil);

                await middlevare.Invoke(context);
            });

            return Task.CompletedTask;
        }
    }
}
