namespace TDDLesson.BankAccount;

public sealed class BankAccount
{
    public int Balance { get; private set; }
    public int Limit { get; private set; }

    public BankAccount(int initialAmount, int limit = 0)
    {
        if (initialAmount < 0) throw new InvalidOperationException("Cannot create account with negative balance");
        if (limit < 0) throw new InvalidOperationException("Cannot create account with negative limit");
        Balance = initialAmount;
        Limit = limit;
    }

    public void Add(int money)
    {
        if (money < 0) throw new InvalidOperationException("Incorrect value");
        
        Balance += money;
    }
    
    public void Withdraw(int money)
    {
        if (money < 0) throw new InvalidOperationException("Incorrect value");

        if (Balance + Limit < money) throw new InvalidOperationException("Not enough balance");
        
        Balance -= money;

        if (money > Balance)
            Limit -= money - Balance;
    }
}