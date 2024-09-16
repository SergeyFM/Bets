
public interface IWalletService {
    decimal GetBalance(int userId);
    void AddFunds(int userId, decimal amount);
    void WithdrawFunds(int userId, decimal amount);
}

public class FakeWalletService : IWalletService {
    public decimal GetBalance(int userId) {
        return 1000m; // Fake balance
    }

    public void AddFunds(int userId, decimal amount) {
        // Fake implementation: Do nothing
    }

    public void WithdrawFunds(int userId, decimal amount) {
        // Fake implementation: Do nothing
    }
}
