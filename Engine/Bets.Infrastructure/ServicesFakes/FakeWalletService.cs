
using System;

public interface IWalletService {
    decimal GetBalance(int userId);
    void AddFunds(int userId, decimal amount);
    void WithdrawFunds(int userId, decimal amount);
}

public class FakeWalletService : IWalletService {
    private decimal balance = 1000m; // Mock initial balance

    public decimal GetBalance(int userId) {
        return balance; // Return mock balance
    }

    public void AddFunds(int userId, decimal amount) {
        balance += amount;
        Console.WriteLine($"Added {amount} to User ID {userId}'s wallet. New balance: {balance}");
    }

    public void WithdrawFunds(int userId, decimal amount) {
        if (balance >= amount) {
            balance -= amount;
            Console.WriteLine($"Withdrawn {amount} from User ID {userId}'s wallet. New balance: {balance}");
        }
        else {
            Console.WriteLine($"Insufficient funds for User ID {userId}. Current balance: {balance}");
        }
    }
}
