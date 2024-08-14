using System;
using System.Threading.Tasks;
using Bets.Application.Services;
using Bets.Core.Models;

namespace Bets.Infrastructure.ServicesMocks;

public class MockWalletService : IWalletService {
    private readonly Wallet _wallet = new() {
        Id = Guid.NewGuid(),
        UserId = Guid.NewGuid(),
        Balance = 1000.0m,
        LastUpdated = DateTime.UtcNow
    };

    public Task<Wallet> GetWalletByUserIdAsync(Guid userId) =>
        // Simplified: always return the same wallet for any user.
        Task.FromResult(_wallet);

    public Task<bool> DepositAsync(Guid userId, decimal amount) {
        _wallet.Balance += amount;
        _wallet.LastUpdated = DateTime.UtcNow;
        return Task.FromResult(true);
    }

    public Task<bool> WithdrawAsync(Guid userId, decimal amount) {
        if (_wallet.Balance >= amount) {
            _wallet.Balance -= amount;
            _wallet.LastUpdated = DateTime.UtcNow;
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }
}
