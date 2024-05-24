namespace TDDLesson.BankAccount;

public sealed class BankAccount
{
    public BankAccount(int limit = 0)
    {
        if (limit < 0)
            throw new InvalidOperationException();
        
        Limit = limit;
    }
    public int Balance { get; private set; }

    public int Limit { get; }

    public void Add(int money)
    {
        if (money < 0) throw new InvalidOperationException();
        
        Balance += money;
    }
    
    public void Take(int money)
    {
        if (money < 0) throw new InvalidOperationException();

        var allMoney = Balance + Limit;
        if (allMoney < money)
            throw new InvalidOperationException("Money not enough");
        
        Balance -= money;
    }
}