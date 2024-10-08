
public interface INotificationService {
    void SendBetConfirmation(int userId, int betId);
    void SendBetCancellation(int userId, int betId);
}

public class FakeNotificationService : INotificationService {
    public void SendBetConfirmation(int userId, int betId) {
        // Fake implementation: Do nothing
    }

    public void SendBetCancellation(int userId, int betId) {
        // Fake implementation: Do nothing
    }
}
