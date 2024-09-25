using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using UserServer.Core.DTO;
using UserServer.Core.Entities;
using UserServer.Core.Interfaces;

namespace UserServer.WebHost.Controllers.V1
{
    [ApiVersion("1")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Метод для входа
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResult>> Login(LoginDto loginDto)
        {
            try
            {
                var result = await _authenticationService.LoginAsync(loginDto);
                
                return Ok(result);
            }
            catch (InvalidCredentialException)
            {
                return Unauthorized(new { Message = "Invalid email or password." });
            }
        }

        /// <summary>
        /// Метод для выхода
        /// </summary>
        /// <returns></returns>
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Получаем Id текущего пользователя
            await _authenticationService.LogoutAsync(userId);

            return NoContent();
        }

        /// <summary>
        /// Метод для обновления токена
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        [HttpPost("refresh-token")]
        public async Task<ActionResult<AuthenticationResult>> RefreshToken(string refreshToken)
        {
            try
            {
                var result = await _authenticationService.RefreshTokenAsync(refreshToken);
            
                return Ok(result);
            }
            catch (InvalidOperationException)
            {
                return Unauthorized(new { Message = "Invalid refresh token." });
            }
        }

        [HttpGet("validateToken")]
        [Authorize]
        public IActionResult ValidateToken()
        {
            var roles = User.Claims
                    .Where(c => c.Type == ClaimTypes.Role)
                    .Select(c => c.Value)
                    .ToList();

            //var claims = User.Claims.ToList();
            if (!roles.Any()) 
            {
                return Unauthorized();
            }

            return Ok(roles);
        }

    }
}
