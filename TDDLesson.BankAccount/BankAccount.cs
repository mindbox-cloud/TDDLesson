namespace TDDLesson.BankAccount;

public record BankAccount
{
    public BankAccount(int balance = 0)
    {
        Balance = balance;
    }

    public int Balance { get; private set; }

    public BankAccount Add(int money)
    {
        if (money < 0) throw new ArgumentException();
        return new BankAccount(Balance + money);
    }
    
    public BankAccount Withdraw(int money)
    {
        if (money < 0) throw new ArgumentException();
        if (money > Balance) throw new InvalidOperationException();
        
        return new BankAccount(Balance - money);
    }
}