
using System;

public interface INotificationService {
    void SendBetConfirmation(int userId, int betId);
    void SendBetCancellation(int userId, int betId);
}

public class FakeNotificationService : INotificationService {
    public void SendBetConfirmation(int userId, int betId) {
        // Log or print a message that a notification would be sent
        Console.WriteLine($"Confirmation sent for User ID {userId} and Bet ID {betId}");
    }

    public void SendBetCancellation(int userId, int betId) {
        // Log or print a message that a notification would be sent
        Console.WriteLine($"Cancellation sent for User ID {userId} and Bet ID {betId}");
    }
}
