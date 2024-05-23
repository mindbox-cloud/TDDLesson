namespace TDDLesson.BankAccount.Tests;

using FluentAssertions;

/*
 * - Добавление на счет денеждых средств
 * - Снятие средств со счета
 */


[TestClass]
public sealed class BankAccountTests
{
    [TestMethod]
    public void ShouldBe20OnAccount_When20Added()
    {
        // Arrange
        var balance = 20;
        var account = new BankAccount(0);
        
        // Act
        var newAccount = account.Add(balance);
        
        // Assert
        newAccount.Balance.Should().Be(balance);
    }
    
    [TestMethod]
    public void ShouldBe50OnAccount_When20AddedAnd30Was()
    {
        // Arrange
        var balanceBefore = 30;
        var balanceAdded = 20;
        var account = new BankAccount(balanceBefore);
        
        // Act
        var newAccount = account.Add(balanceAdded);
        
        // Assert
        newAccount.Balance.Should().Be(50);
    }
    
    [TestMethod]
    public void ShouldThrow_WhenNegativeValueAdded()
    {
        // Arrange
        var balanceBefore = 30;
        var balanceAdded = -20;
        var account = new BankAccount(balanceBefore);
        
        // Act
        var newAccount = () => account.Add(balanceAdded);
        
        // Assert
        newAccount.Should().ThrowExactly<ArgumentException>();
    }
    
    [TestMethod]
    public void ShouldBe20OnAccount_When10Withdrawn()
    {
        // Arrange
        var balanceBefore = 30;
        var balance = 10;
        var account = new BankAccount(0, balanceBefore);
        
        // Act
        var newAccount = account.Withdraw(balance);
        
        // Assert
        newAccount.Balance.Should().Be(20);
    }
    
    [TestMethod]
    public void ShouldThrow_WhenNegativeValueWithdrawn()
    {
        // Arrange
        var balanceBefore = 30;
        var balanceWithdrawn = -20;
        var account = new BankAccount(0, balanceBefore);
        
        // Act
        var newAccount = () => account.Withdraw(balanceWithdrawn);
        
        // Assert
        newAccount.Should().ThrowExactly<ArgumentException>();
    }
    
    [TestMethod]
    public void ShouldThrow_WhenWithdrawnMoreThanOnAccountIncludingOverdraft()
    {
        // Arrange
        var balanceOverdraft = 10;
        var balanceBefore = 30;
        var balanceWithdrawn = 50;
        var account = new BankAccount(0, balanceBefore);
        
        // Act
        var newAccount = () => account.Withdraw(balanceWithdrawn);
        
        // Assert
        newAccount.Should().ThrowExactly<InvalidOperationException>();
    }
        
    [TestMethod]
    public void ShouldBeMinus50OnAccount_WhenWithdrawn50AndZeroOnAccount()
    {
        // Arrange
        var balanceBefore = 10;
        var balanceWithdrawn = 50;
        var account = new BankAccount(0, balanceBefore);
        
        // Act
        var newAccount = account.Withdraw(balanceWithdrawn);
        
        // Assert
        newAccount.Balance.Should().Be(-40);
    }
}