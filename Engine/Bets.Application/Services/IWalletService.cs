using System;
using System.Threading.Tasks;
using Bets.Core.Models;

namespace Bets.Application.Services;
public interface IWalletService {
    Task<Wallet> GetWalletByUserIdAsync(Guid userId);
    Task<bool> DepositAsync(Guid userId, decimal amount);
    Task<bool> WithdrawAsync(Guid userId, decimal amount);
}
