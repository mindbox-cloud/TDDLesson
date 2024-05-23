namespace TDDLesson.BankAccount;

public record BankAccount(int Overdraft, int Balance = 0)
{
    public int Balance { get; private set; } = Balance;

    public BankAccount Add(int money)
    {
        if (money < 0) throw new ArgumentException();
        return new BankAccount(Balance + money);
    }
    
    public BankAccount Withdraw(int money)
    {
        if (money < 0) throw new ArgumentException();
        if (Balance + Overdraft - money <  0) throw new InvalidOperationException();
        return new BankAccount(Balance + Overdraft - money, Overdraft);
    }
}