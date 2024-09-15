using UserServer.Core.Service;

namespace UserServer.WebHost.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JwtUtils _jwtUtils;

        public JwtMiddleware(RequestDelegate next, JwtUtils jwtUtils)
        {
            _next = next;
            _jwtUtils = jwtUtils;
        }
        public async Task Invoke(HttpContext context) 
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                var principal = _jwtUtils.ValidateToken(token);
                if (principal != null)
                {
                    context.User = principal; // Устанавливаем пользователя в контекст
                }
            }

            await _next(context);
        }
    }
}
