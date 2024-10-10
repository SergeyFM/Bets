using System.Net.Http.Headers;
using System.Security.Claims;

namespace TestAuthApiGateway.Middleware
{
    public class RoleMiddlewareValidateUserServer
    {
        private readonly RequestDelegate _next;
        private readonly IHttpClientFactory _httpClientFactory;

        public RoleMiddlewareValidateUserServer(RequestDelegate next, IHttpClientFactory httpClientFactory)
        {
            _next = next;
            _httpClientFactory = httpClientFactory;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!string.IsNullOrEmpty(token))
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetAsync("http://userserver:8080/api/v1/Auth/validateToken");
                if (response.IsSuccessStatusCode)
                {
                    //var claims = await response.Content.ReadFromJsonAsync<IEnumerable<Claim>>();
                    var roles = await response.Content.ReadFromJsonAsync<IEnumerable<string>>();
                    var claims = roles?.Select(role => new Claim(ClaimTypes.Role, role));
                    context.User = new ClaimsPrincipal(new ClaimsIdentity(claims));
                }
                else 
                {
                    var message = await response.Content.ReadAsStringAsync();
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return;
                }
            }

            await _next(context);
        }
    }
}
