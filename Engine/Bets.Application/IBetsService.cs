using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bets.Core.Models;

namespace Bets.Application;
public interface IBetsService {
    Task<Bet> PlaceBetAsync(Bet bet);
    Task<IEnumerable<Bet>> GetBetsByUserIdAsync(Guid userId);
    Task<Bet> GetBetByIdAsync(Guid betId);
}
