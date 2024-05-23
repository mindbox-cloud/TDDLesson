namespace TDDLesson.BankAccount;

public sealed class BankAccount
{
    public BankAccount(int balance = 0, int limit = 0)
    {
        Balance = balance;
        Limit = limit;
    }
    
    public int Balance { get; private set; }
    
    public int Limit { get; init; }

    public void Add(int money)
    {
        if (money < 0)
            throw new ArgumentException("The amount must be greater than zero");
        
        Balance += money;
    }

    public void Withdrawals(int money)
    {
        if (money < 0)
            throw new ArgumentException("The amount must be greater than zero");

        if (Balance - money < Limit)
            throw new InvalidOperationException();

        Balance -= money;
    }
}