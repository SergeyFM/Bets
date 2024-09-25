using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace TestAuthApiGateway.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UsersController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")] // Ограничиваем доступ только для администраторов
        public async Task<IActionResult> GetUsers()
        {
            var client = _httpClientFactory.CreateClient();

            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync("http://userserver:8080/api/v1/Users");

            if (response.IsSuccessStatusCode)
            {
                var users = await response.Content.ReadFromJsonAsync<IEnumerable<UserDto>>();
                return Ok(users);
            }

            return Unauthorized();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // Ограничиваем доступ только для администраторов
        public async Task<IActionResult> DeleteUser(string id)
        {
            var client = _httpClientFactory.CreateClient();

            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.DeleteAsync($"http://userserver:8080/api/v1/users/{id}");

            if (response.IsSuccessStatusCode)
            {
                return NoContent();
            }

            return Unauthorized();
        }
    }
}
