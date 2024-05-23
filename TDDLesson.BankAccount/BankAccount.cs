namespace TDDLesson.BankAccount;

public sealed class BankAccount
{
    public int Balance { get; private set; }

    public BankAccount(int balance = 0)
    {
        Balance = balance;
    }

    public void Add(int money)
    {
        ValidateMoney(money);
        
        Balance += money;
    }

    public void Take(int money)
    {
        ValidateMoney(money);
        
        if (Balance < money) throw new InvalidOperationException();
        
        Balance -= money;
    }

    private void ValidateMoney(int money)
    {
        if (money <= 0) throw new InvalidOperationException("Money should be more than zero");
    }
}