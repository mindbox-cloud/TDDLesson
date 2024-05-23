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
        // Arrange
        var account = new BankAccount();
        account.Add(money);
        
        // Act
        account.Add(money);
        
        // Assert
        Assert.AreEqual(expected, account.Balance);
    }
    
    [TestMethod]
    [DataRow(-1)]
    public void AddNegativeMoneyToBalance_ShouldThrowArgumentException(int money)
    {
        // Arrange
        var account = new BankAccount();
        
        // Act + Assert
        Assert.ThrowsException<ArgumentException>(() => account.Add(money));
    }
    
    [TestMethod]
    [DataRow(5, 5, 0)]
    public void WithdrawalsPositiveMoneyFromBalance_MoneyDecreased(int initialBalance, int money, int expected)
    {
        // Arrange
        var account = new BankAccount();
        account.Add(money);
        
        // Act
        account.Withdrawals(money);
        
        // Assert
        Assert.AreEqual(expected, account.Balance);
    }
    
    [TestMethod]
    [DataRow(5, -5)]
    public void WithdrawalsNegativeMoneyFromBalance_ShouldThrowArgumentException(int initialBalance, int money)
    {
        // Arrange
        var account = new BankAccount();
        account.Add(money);
        
        // Act + Assert
        Assert.ThrowsException<ArgumentException>(() => account.Withdrawals(money));
        Assert.AreEqual(initialBalance, account.Balance);
    }
    
    [TestMethod]
    [DataRow(5)]
    public void WithdrawalsMoneyFromBalance_BalanceLessThanWithdrawalAmount_ShouldThrowInvalidOperationException(int money)
    {
        // Arrange
        var account = new BankAccount();
        
        // Act + Assert
        Assert.ThrowsException<InvalidOperationException>(() => account.Withdrawals(money));
    }
}