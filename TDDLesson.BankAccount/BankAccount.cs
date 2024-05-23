namespace TDDLesson.BankAccount;

public sealed class BankAccount
{
    public int Balance { get; private set; }
    public int Overdraft { get; private set; }

    public BankAccount(int balance = 0, int overdraft = 0)
    {
        Balance = balance;
        Overdraft = overdraft;
    }

    public void Add(int money)
    {
        ValidateMoney(money);
        
        Balance += money;
    }

    public void Withdraw(int money)
    {
        ValidateMoney(money);
        
        if (Balance + Overdraft < money) throw new InvalidOperationException("Недостаточно деняк");
        
        Balance -= money;
    }

    private void ValidateMoney(int money)
    {
        if (money <= 0) throw new InvalidOperationException("Money should be more than zero");
    }
}