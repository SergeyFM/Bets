using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using UserServer.Core.DTO;
using UserServer.Core.Interfaces;
using UserServer.DataAccess.Extensions;
using UserServer.WebHost.Classes;

namespace UserServer.WebHost.Controllers.V1
{
    [ApiVersion("1")]
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RolesController : BaseContoller
    {
        private readonly IRoleService _roleService;
        private readonly IDistributedCache _cache;
        private const string DefaultPreficsCashe = ".Roles.";

        public RolesController(IRoleService roleService, IDistributedCache cache, ILogger<BaseContoller> logger) : base(logger)
        {
            _roleService = roleService;
            _cache = cache;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetRoles()
        {
            var cacheKey = DefaultPreficsCashe + "All";

            var roles = await _cache.GetOrSerAsync(cacheKey, 
                async () => await _roleService.GetAllRolesAsync(), 
                TimeSpan.FromMinutes(30));

            LogInformationByUser("Запросил все роли");

            return Ok(roles);
        }

        // Получение ролей пользователя
        [HttpGet("{userId}/roles")]
        public async Task<ActionResult<IEnumerable<string>>> GetUserRoles(string userId)
        {
            var cachKey = DefaultPreficsCashe + "User." + userId;
            var roles = await _cache.GetOrSerAsync(cachKey,
                async () => await _roleService.GetUserRolesAsync(userId));

            LogInformationByUser($"Запросил все роли пользователя с id = {userId}");

            return Ok(roles);
        }

        // Получение пользователей в роли
        [HttpGet("users-in-role/{roleName}")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersInRole(string roleName)
        {
            var cachKey = DefaultPreficsCashe + "Role." + roleName;
            var users = await _cache.GetOrSerAsync(cachKey,
                async () => await _roleService.GetUsersInRoleAsync(roleName),
                TimeSpan.FromMinutes(15)
            );

            LogInformationByUser($"Запросил пользователей для роли = {roleName}");

            return Ok(users);
        }

        [HttpPost]
        public async Task<ActionResult<RoleDto>> CreateRole(string roleName)
        {
            var createdRole = await _roleService.CreateRoleAsync(roleName);
            LogInformationByUser($"Создал роль = {roleName}");
            return CreatedAtAction(nameof(GetRoles), createdRole);
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> AddUserToRole(string userId, RoleDto role)
        {
            var result = await _roleService.AddUserToRoleAsync(userId, role);
            if (result.Succeeded) 
            {
                LogInformationByUser($"Назначил для {userId} роль - {role.Name}");
                return NoContent();
            }

            return BadRequestHttp($"Failed to add user to role: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }

        [HttpDelete("{userId}&roles={roleName}")]
        public async Task<IActionResult> RemoveUserFromRole(string userId, RoleDto role)
        {
            var result = await _roleService.RemoveUserFromRoleAsync(userId, role);
            if (result.Succeeded) 
            {
                LogInformationByUser($"Удалил для {userId} роль - {role.Name}");
                return NoContent();
            }

            return BadRequestHttp($"Failed to remove user from role: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }
    }
}
