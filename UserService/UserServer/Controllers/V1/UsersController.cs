using Asp.Versioning;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using UserServer.Core.DTO;
using UserServer.Core.Entities;
using UserServer.Core.Interfaces;
using UserServer.DataAccess.Extensions;
using UserServer.WebHost.Classes;

namespace UserServer.WebHost.Controllers.V1
{
    [ApiVersion("1")]
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : BaseContoller
    {
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly IDistributedCache _cache;
        private const string DefaultPreficsCashe = ".Users.";
        private readonly IBus _bus;

        public UsersController(IUserService userService, UserManager<User> userManager,
            IDistributedCache cache, ILogger<BaseContoller> logger, IBus bus)
            : base(logger)
        {
            _userService = userService;
            _userManager = userManager;
            _cache = cache;
            _bus = bus;
        }

        /// <summary>
        /// Получить всех пользователей (доступно только для администраторов)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsers()
        {
            var cachKey = DefaultPreficsCashe + "All";
            var users = await _cache.GetOrSerAsync(cachKey,
                async () => await _userService.GetAllUsersAsync(),
                TimeSpan.FromMinutes(30));

            LogInformationByUser("Запросил всех пользователей");

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
            var cachKey = DefaultPreficsCashe + "UserId." + id;
            var user = await _cache.GetOrSerAsync(cachKey,
                async () => await _userService.GetUserByIdAsync(id),
                TimeSpan.FromMinutes(10)
            );
            
            LogInformationByUser($"Запросил информацию по пользователю {id}");

            if (user == null) return NotFound("Пользователь не найден");

            return Ok(user);
        }

        /// <summary>
        /// Получить пользователя по имени
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet("userName/{userName}")]
        public async Task<ActionResult<ResponceUserDto>> GetUsersByUserName(string userName)
        {
            var cachKey = DefaultPreficsCashe + "UserName." + userName;
            var user = await _cache.GetOrSerAsync(cachKey,
                async () => await _userService.GetUserByUserName(userName),
                TimeSpan.FromMinutes(10)
            );

            LogInformationByUser($"Запросил информацию по пользователю {userName}");

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
                LogInformationByUser($"Создал нового юзера {user.Email}", user.Email);
                var responseUserDto = await _userService.GetUserByUserName(user.Email);
                if (responseUserDto != null)
                {
                    await _bus.Publish(responseUserDto);
                }

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
            await _userService.UpdateUserAsync(user);

            LogInformationByUser($"Обновил информацию о пользователе {id}: {JsonSerializer.Serialize(user)}");

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

            LogInformationByUser($"Удалил пользователя: {id}");

            return NoContent();
        }
    }
}
