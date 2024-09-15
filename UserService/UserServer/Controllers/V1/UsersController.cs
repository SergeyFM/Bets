using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserServer.Core.DTO;
using UserServer.Core.Entities;
using UserServer.Core.Interfaces;

namespace UserServer.WebHost.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;

        public UsersController(IUserService userService, UserManager<User> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        /// <summary>
        /// Получить всех пользователей (доступно только для администраторов)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        /// <summary>
        /// Получить пользователя по ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponceUserDto>> GetUserById(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound("Пользователь не найден");

            return Ok(user);
        }

        [HttpGet("userName={userName}")]
        public async Task<ActionResult<ResponceUserDto>> GetUsersByUserName(string userName)
        {
            var user = await _userService.GetUserByUserName(userName);
            if (user == null) return NotFound("Пользователь не найден");

            return Ok(user);
        }

        /// <summary>
        /// Регистрация нового пользователя (доступно для всех)
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<ResponceUserDto>> RegisterUser(UserDto user, string password)
        {
            
            var result = await _userService.CreateUserAsync(user, password);
            if (result.Succeeded)
            {
                return CreatedAtAction(nameof(GetUsersByUserName), new { userName = user.UserName }, user);
            }

            return BadRequest(result.Errors);
        }

        /// <summary>
        /// Обновление пользователя (доступно только для администраторов)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")] // Ограничиваем доступ только для администраторов
        public async Task<IActionResult> UpdateUser(string id, UserDto user)
        {
            //if (id != user.Id)
            //{
            //    return BadRequest();
            //}

            await _userService.UpdateUserAsync(user);
            
            return Ok();
        }

        /// <summary>
        /// Удаление пользователя (доступно только для администраторов)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // Ограничиваем доступ только для администраторов
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _userService.DeleteUserAsync(id);
            
            return NoContent();
        }
    }
}
