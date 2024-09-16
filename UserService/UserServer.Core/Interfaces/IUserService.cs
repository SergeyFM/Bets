using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserServer.Core.DTO;
using UserServer.Core.Entities;

namespace UserServer.Core.Interfaces
{
    public interface IUserService
    {
        Task<ResponceUserDto> GetUserByIdAsync(string userId);
        Task<IEnumerable<ResponceUserDto>> GetAllUsersAsync();
        //Task RegisterUserAsync(RegisterUserDto userDto);
        Task UpdateUserAsync(UserDto user);
        Task DeleteUserAsync(string userId);
        Task<IdentityResult> CreateUserAsync(UserDto userDto, string password);
        Task<ResponceUserDto> GetUserByUserName(string userName);
    }
}
