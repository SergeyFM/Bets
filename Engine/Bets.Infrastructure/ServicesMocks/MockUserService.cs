using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bets.Application.Services;
using Bets.Core.Models;

namespace Bets.Infrastructure.ServicesMocks;

public class MockUserService : IUserService {
    private readonly List<User> _users = new() {
        new User { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", IsEmailConfirmed = true },
        new User { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", IsEmailConfirmed = false }
    };

    public Task<User> GetUserByIdAsync(Guid userId) => Task.FromResult(_users.Find(user => user.Id == userId));

    public Task<IEnumerable<User>> GetUsersByRoleAsync(string role) =>
        // Simplified example, assuming all users have the same role.
        Task.FromResult((IEnumerable<User>)_users);

    public Task<bool> CreateUserAsync(User user) {
        _users.Add(user);
        return Task.FromResult(true);
    }

    public Task<bool> UpdateUserAsync(User user) {
        User? existingUser = _users.Find(u => u.Id == user.Id);
        if (existingUser != null) {
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.IsEmailConfirmed = user.IsEmailConfirmed;
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    public Task<bool> DeleteUserAsync(Guid userId) {
        User? user = _users.Find(u => u.Id == userId);
        if (user != null) {
            _users.Remove(user);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }
}
