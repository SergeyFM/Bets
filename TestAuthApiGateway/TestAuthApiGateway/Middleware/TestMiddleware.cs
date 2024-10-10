namespace TestAuthApiGateway.Middleware
{
    public class TestMiddleware
    {
        private readonly RequestDelegate _next;

        public TestMiddleware(RequestDelegate requestDelegate)
        {
            _next = requestDelegate;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue("Authorization", out var token))
            {
                var user = context.User;
                var isAuthenticated = user?.Identity?.IsAuthenticated;
                var userClaims = user?.Claims;
            }
            await _next(context);
        }
    }
}