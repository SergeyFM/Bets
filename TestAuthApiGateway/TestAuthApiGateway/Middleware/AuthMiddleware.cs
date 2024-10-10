using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace TestAuthApiGateway.Middleware
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue("Authorization", out var token))
            {
                var jwtToken = token.ToString().Replace("Bearer ", "");
                var handler = new JwtSecurityTokenHandler();

                var jwtTokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };

                try
                {
                    var claimsPrincipal = handler.ValidateToken(jwtToken, jwtTokenValidationParameters, out var validatedToken);
                    context.User = claimsPrincipal;
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                    Console.WriteLine($"Token validation failed: {message}");
                }
            }

            await _next(context);
        }
    }
}
