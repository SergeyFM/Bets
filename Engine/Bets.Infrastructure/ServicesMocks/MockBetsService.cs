using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bets.Application.Services;
using Bets.Core.Models;

namespace Bets.Infrastructure.ServicesMocks;

public class MockBetsService : IBetsService {
    private readonly List<Bet> _bets = new() {
        new Bet { Id = Guid.NewGuid(), UserId = Guid.NewGuid(), EventId = Guid.NewGuid(), Amount = 50.0m, Odds = 2.0m, Outcome = "Win", IsSettled = true, PlacedAt = DateTime.UtcNow.AddDays(-1), SettledAt = DateTime.UtcNow }
    };

    public Task<Bet> PlaceBetAsync(Bet bet) {
        bet.Id = Guid.NewGuid();
        _bets.Add(bet);
        return Task.FromResult(bet);
    }

    public Task<IEnumerable<Bet>> GetBetsByUserIdAsync(Guid userId) => Task.FromResult(_bets.FindAll(bet => bet.UserId == userId) as IEnumerable<Bet>);

    public Task<Bet> GetBetByIdAsync(Guid betId) => Task.FromResult(_bets.Find(bet => bet.Id == betId));
}
