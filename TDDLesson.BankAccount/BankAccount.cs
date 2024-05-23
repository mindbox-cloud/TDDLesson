namespace TDDLesson.BankAccount;

public sealed class BankAccount
{
    public int Balance { get; private set; }

    public BankAccount(int balance)
    {
        Balance = balance;
    }

    public void AddMoney(int amount)
    {
        if (amount <= 0)
            throw new InvalidOperationException($"Amount should be positive number.");
        
        Balance += amount;
    }

    public void WithdrawMoney(int amount)
    {
        if (amount > Balance)
            throw new InvalidOperationException($"Not enough money on balance to withdraw.");
        
        Balance -= amount;
    }
}