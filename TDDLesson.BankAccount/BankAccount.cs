namespace TDDLesson.BankAccount;

public sealed class BankAccount
{
    public int Balance { get; private set; }

    public BankAccount(int initialAmount)
    {
        if (initialAmount < 0) throw new InvalidOperationException("Cannot create account with negative balance");
        Balance = initialAmount;
    }

    public void Add(int money)
    {
        if (money < 0) throw new InvalidOperationException("Incorrect value");
        
        Balance += money;
    }
    
    public void Withdraw(int money)
    {
        if (money < 0) throw new InvalidOperationException("Incorrect value");

        if (Balance < money) throw new InvalidOperationException("Not enough balance");
        
        Balance -= money;
    }
}