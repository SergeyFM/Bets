
using System;
using System.Collections.Generic;

public interface IBetsService {
    IEnumerable<BetEvent> GetAllEvents();
    BetEvent GetEventDetails(int eventId);
    void PlaceBet(int eventId, int outcomeId, decimal amount);
    void CancelBet(int betId);
}

public class FakeBetsService : IBetsService {
    public IEnumerable<BetEvent> GetAllEvents() {
        return
        [
            new BetEvent { Id = 1, Name = "Кто победит в президентских выборах в США?", Participants = 9, BankSize = 35000 },
            new BetEvent { Id = 2, Name = "Курс RUR/USD – будет ли больше 100 руб. к 1 февраля 2025 года?", Participants = 15, BankSize = 45000 }
        ];
    }

    public BetEvent GetEventDetails(int eventId) {
        return new BetEvent { Id = eventId, Name = "Кто победит в президентских выборах в США?", Participants = 9, BankSize = 35000 };
    }

    public void PlaceBet(int eventId, int outcomeId, decimal amount) {
        Console.WriteLine($"Bet placed on event {eventId}, outcome {outcomeId} with amount {amount}");
    }

    public void CancelBet(int betId) {
        Console.WriteLine($"Bet {betId} cancelled");
    }
}
