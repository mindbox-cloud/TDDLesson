namespace TDDLesson.BankAccount;

public sealed class BankAccount
{
    public int Balance { get; private set; }
    public int Limit { get; private set; }

    public BankAccount(int balance, int limit = 0)
    {
        if (limit < 0)
            throw new ArgumentException("Limint cannot be negative");
        
        Balance = balance;
        Limit = limit;
    }

    public void AddMoney(int amount)
    {
        if (amount <= 0)
            throw new InvalidOperationException($"Amount should be positive number.");
        
        Balance += amount;
    }

    public void WithdrawMoney(int amount)
    {
        if (amount > (Balance + Limit))
            throw new InvalidOperationException($"Not enough money on balance to withdraw.");
        
        if (amount <= 0)
            throw new InvalidOperationException($"Amount should be positive number.");
        
        Balance -= amount;
    }
}