using System;

namespace Bets.Core.Models;
#nullable disable
public class Wallet {
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public decimal Balance { get; set; }
    public DateTime LastUpdated { get; set; }
}
