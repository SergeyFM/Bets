using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TestAuthApiGateway.Middleware
{
    public class RoleMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JwtSettings _jwtSettings;

        public RoleMiddleware(RequestDelegate next, IOptions<JwtSettings> jwtSettings)
        {
            _next = next;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);
                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidIssuer = _jwtSettings.Issuer,
                        ValidateAudience = true,
                        ValidAudience = _jwtSettings.Audience,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    }, out SecurityToken validatedToken);

                    var jwtToket = (JwtSecurityToken)validatedToken;
                    context.User = new ClaimsPrincipal(new ClaimsIdentity(jwtToket.Claims));
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                    return;
                }
            }

            await _next(context); // Передаем управление следующему middleware
        }
    }
}
