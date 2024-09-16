using Microsoft.AspNetCore.Identity;
using UserServer.Core.DTO;

namespace UserServer.Core.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetAllRolesAsync();
        Task<RoleDto> CreateRoleAsync(string roleName);
        Task<IdentityResult> AddUserToRoleAsync(string userId, RoleDto role);
        Task<IdentityResult> RemoveUserFromRoleAsync(string userId, RoleDto role);
        Task<IEnumerable<string>> GetUserRolesAsync(string userId); // Получение ролей пользователя
        Task<IEnumerable<UserDto>> GetUsersInRoleAsync(string roleName); // Получение пользователей в роли
    }
}
