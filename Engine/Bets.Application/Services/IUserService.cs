using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bets.Core.Models;

namespace Bets.Application.Services;
public interface IUserService {
    Task<User> GetUserByIdAsync(Guid userId);
    Task<IEnumerable<User>> GetUsersByRoleAsync(string role);
    Task<bool> CreateUserAsync(User user);
    Task<bool> UpdateUserAsync(User user);
    Task<bool> DeleteUserAsync(Guid userId);
}
