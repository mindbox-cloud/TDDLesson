namespace TDDLesson.BankAccount;

public sealed class BankAccount
{
    public int Balance { get; set; }

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

        if (Balance < money)
            throw new InvalidOperationException("You are trying to take off more than you have.");

        Balance -= money;
    }
}