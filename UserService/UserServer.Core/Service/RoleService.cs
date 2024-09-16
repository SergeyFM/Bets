using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserServer.Core.DTO;
using UserServer.Core.Entities;
using UserServer.Core.Interfaces;

namespace UserServer.Core.Service
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;

        public RoleService(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
        {
            
            return await _roleManager.Roles.Select(x => new RoleDto 
            {
                ID = x.Id,
                Name = x.Name
            }).ToListAsync();
        }

        public async Task<RoleDto> CreateRoleAsync(string roleName)
        {
            var role = new Role { Name = roleName };
            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                return new RoleDto { ID = role.Id, Name = role.Name };
            }

            throw new Exception($"Failed to create role: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }

        public async Task<IdentityResult> AddUserToRoleAsync(string userId, RoleDto role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) throw new Exception($"User not found.");

            var result = await _userManager.AddToRoleAsync(user, role.Name);

            return result;
        }

        public async Task<IdentityResult> RemoveUserFromRoleAsync(string userId, RoleDto role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) throw new Exception("User not found.");

            var result = await _userManager.RemoveFromRoleAsync(user, role.Name);

            return result;
        }

        public async Task<IEnumerable<string>> GetUserRolesAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) throw new Exception("User not found.");

            return await _userManager.GetRolesAsync(user);
        }

        public async Task<IEnumerable<UserDto>> GetUsersInRoleAsync(string roleName)
        {
            var usersInRole = await _userManager.GetUsersInRoleAsync(roleName);

            return usersInRole.Select(u => new UserDto
            {
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                UserName = u.UserName
            }).ToList();
        }
    }
}