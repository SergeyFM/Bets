using System;

namespace Bets.Core.Models;
#nullable disable
public class Bet {
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid EventId { get; set; }
    public decimal Amount { get; set; }
    public decimal Odds { get; set; }
    public string Outcome { get; set; }
    public DateTime PlacedAt { get; set; }
    public DateTime? SettledAt { get; set; }
    public bool IsSettled { get; set; }
}
