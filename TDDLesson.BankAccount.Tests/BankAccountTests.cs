namespace TDDLesson.BankAccount.Tests;

using FluentAssertions;

[TestClass]
public sealed class BankAccountTests
{
    [TestMethod]
    [DataRow(0, 5, 5)]
    [DataRow(5, 5, 10)]
    public void AddPositiveMoneyToBalance_MoneyAdded(int initialBalance, int money, int expected)
    {
        var account = new BankAccount
        {
            Balance = initialBalance
        };
        
        account.Add(money);
        
        Assert.AreEqual(expected, account.Balance);
    }
    
    [TestMethod]
    [DataRow(-1)]
    public void AddNegativeMoneyToBalance_ShouldThrowArgumentException(int money)
    {
        var account = new BankAccount();
        
        Assert.ThrowsException<ArgumentException>(() => account.Add(money));
    }
    
    [TestMethod]
    [DataRow(5, 5, 0)]
    public void WithdrawalsPositiveMoneyFromBalance_MoneyDecreased(int initialBalance, int money, int expected)
    {
        var account = new BankAccount
        {
            Balance = initialBalance
        };
        
        account.Withdrawals(money);
        
        Assert.AreEqual(expected, account.Balance);
    }
    
    [TestMethod]
    [DataRow(5, -5)]
    public void WithdrawalsNegativeMoneyFromBalance_ShouldThrowArgumentException(int initialBalance, int money)
    {
        var account = new BankAccount
        {
            Balance = initialBalance
        };
        
        Assert.ThrowsException<ArgumentException>(() => account.Withdrawals(money));
        Assert.AreEqual(initialBalance, account.Balance);
    }
    
    [TestMethod]
    [DataRow(5)]
    public void WithdrawalsMoneyFromBalance_BalanceEqualsZero_ShouldThrowInvalidOperationException(int money)
    {
        var account = new BankAccount();
        
        Assert.ThrowsException<InvalidOperationException>(() => account.Withdrawals(money));
    }
}