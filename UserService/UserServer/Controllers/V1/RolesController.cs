using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserServer.Core.DTO;
using UserServer.Core.Interfaces;

namespace UserServer.WebHost.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetRoles()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Ok(roles);
        }

        [HttpPost]
        public async Task<ActionResult<RoleDto>> CreateRole(string roleName)
        {
            var createdRole = await _roleService.CreateRoleAsync(roleName);
            return CreatedAtAction(nameof(GetRoles), createdRole);
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> AddUserToRole(string userId, RoleDto role)
        {
            var result = await _roleService.AddUserToRoleAsync(userId, role);
            if (result.Succeeded) 
            {
                return NoContent();
            }

            return BadRequest($"Failed to add user to role: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }

        [HttpDelete("{userId}&roles={roleName}")]
        public async Task<IActionResult> RemoveUserFromRole(string userId, RoleDto role)
        {
            var result = await _roleService.RemoveUserFromRoleAsync(userId, role);
            if (result.Succeeded) 
            {
                return NoContent();
            }

            return BadRequest($"Failed to remove user from role: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }

        // Получение ролей пользователя
        [HttpGet("{userId}/roles")]
        public async Task<ActionResult<IEnumerable<string>>> GetUserRoles(string userId)
        {
            var roles = await _roleService.GetUserRolesAsync(userId);

            return Ok(roles);
        }

        // Получение пользователей в роли
        [HttpGet("users-in-role/{roleName}")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersInRole(string roleName)
        {
            var users = await _roleService.GetUsersInRoleAsync(roleName);

            return Ok(users);
        }
    }
}
