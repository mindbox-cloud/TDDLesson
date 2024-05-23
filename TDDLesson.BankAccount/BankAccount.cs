namespace TDDLesson.BankAccount;

public sealed class BankAccount
{
    public int Balance { get; private set; }

    public void Add(int money)
    {
        if (money < 0) throw new InvalidOperationException();
        
        Balance += money;
    }
    
    public void Take(int money)
    {
        //if (money < 0) throw new InvalidOperationException();

        if (money > Balance)
            throw new InvalidOperationException("Money not enough");
        
        Balance -= money;
    }
}