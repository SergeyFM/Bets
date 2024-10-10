using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace TestAuthApiGateway.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var client = this._httpClientFactory.CreateClient();

            var uri = "http://userserver:8080/api/v1/Auth/login";

            var response = await client.PostAsJsonAsync(uri, loginDto);

            if (response.IsSuccessStatusCode)
            {
                var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();
                return Ok(tokenResponse);
            }

            return Unauthorized();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var client = _httpClientFactory.CreateClient();

            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.PostAsync($"http://userserver:8080/api/v1/Auth/logout", null);

            if (response.IsSuccessStatusCode)
            {
                return NoContent();
            }

            return Unauthorized();
        }
    }
}