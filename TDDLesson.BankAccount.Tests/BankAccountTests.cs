using Moq;

namespace TDDLesson.BankAccount.Tests;

using FluentAssertions;

[TestClass]
public sealed class BankAccountTests
{
    [TestMethod]
    public void ShouldAddToAccount_WhenValidAmountProvided()
    {
        // Arrange
        var initialAmount = 100;
        var amountToAdd = 200;
        var account = new BankAccount(initialAmount);
        
        // Act
        account.Add(amountToAdd);
        
        // Assert
        account.Balance.Should().Be(300);
    }
    
    [TestMethod]
    public void ShouldThrowException_WhenInvalidAddAmountProvided()
    {
        // Arrange
        var initialAmount = 100;
        var amountToAdd = -200;
        var account = new BankAccount(initialAmount);
        
        // Act
        var act = () => account.Add(amountToAdd);
        
        // Assert
        act.Should().Throw<InvalidOperationException>().WithMessage("Incorrect value");
    }
    
    [TestMethod]
    public void ShouldWithdrawFromAccount_WhenValidAmountProvided()
    {
        // Arrange
        var initialAmount = 200;
        var amountToWithdraw = 100;
        var account = new BankAccount(initialAmount);
        
        // Act
        account.Withdraw(amountToWithdraw);
        
        // Assert
        account.Balance.Should().Be(100);
    }
    
    [TestMethod]
    public void ShouldThrowException_WhenInvalidWithdrawAmountProvided()
    {
        // Arrange
        var initialAmount = 100;
        var amountToWithdraw = -200;
        var account = new BankAccount(initialAmount);
        
        // Act
        var act = () => account.Withdraw(amountToWithdraw);
        
        // Assert
        act.Should().Throw<InvalidOperationException>().WithMessage("Incorrect value");
    }
    
    [TestMethod]
    public void ShouldThrowException_WhenBalanceGreaterThanAmount()
    {
        // Arrange
        var initialAmount = 100;
        var amountToWithdraw = 101;
        var account = new BankAccount(initialAmount);
        
        // Act
        var act = () => account.Withdraw(amountToWithdraw);
        
        // Assert
        act.Should().Throw<InvalidOperationException>().WithMessage("Not enough balance");
    }
    
    [TestMethod]
    public void ShouldThrowException_WhenAccountCreatedWithNegativeBalance()
    {
        // Arrange
        var amount = -1;
        
        // Act
        var act = () => new BankAccount(amount);
        
        // Assert
        act.Should().Throw<InvalidOperationException>().WithMessage("Cannot create account with negative balance");
    }

    [TestMethod]
    public void ShouldCreateBankAccount_WhenAccountCreatedWithZeroBalance()
    {
        // Arrange
        var amount = 0;
        
        // Act
        var account = new BankAccount(amount);
        
        // Assert
        account.Balance.Should().Be(amount);
    }
    
    [TestMethod]
    public void ShouldCreateBankAccount_WhenAccountCreatedWithZeroBalanceAndLimit()
    {
        // Arrange
        var amount = 0;
        var limit = 100;
        
        // Act
        var account = new BankAccount(amount, limit);
        
        // Assert
        account.Limit.Should().Be(limit);
    }
    
    [TestMethod]
    public void ShouldNotCreateBankAccount_WhenAccountCreatedWithZeroBalanceAndNegativeLimit()
    {
        // Arrange
        var amount = 0;
        var limit = -100;
        
        // Act
        var act = () => new BankAccount(amount, limit);
        
        // Assert
        act.Should().Throw<InvalidOperationException>().WithMessage("Cannot create account with negative limit");
    }
    
    [TestMethod]
    public void ShouldWithdrawIncludeLimit_WhenMoneyInLimit()
    {
        // Arrange
        var amount = 100;
        var limit = 100;
        var sum = amount + limit;
        var account = new BankAccount(amount, limit);
        
        // Act
        account.Withdraw(sum);
        
        // Assert
        account.Balance.Should().Be(-100);
    }
    
    [TestMethod]
    public void ShouldWithdrawIncludeLimit_WhenMoneyOutOfLimit()
    {
        // Arrange
        var amount = 100;
        var limit = 100;
        var sum = amount + limit + 100;
        var account = new BankAccount(amount, limit);
        
        // Act
        var act = () => account.Withdraw(sum);
        
        // Assert
        act.Should().Throw<InvalidOperationException>().WithMessage("Not enough balance");
    }
    
    [TestMethod]
    public void ShouldDecreaseLimit_WhenWithdrawMoreThanBalance()
    {
        // Arrange
        var amount = 100;
        var limit = 100;
        var sum = amount + limit;
        var account = new BankAccount(amount, limit);
        
        // Act
        account.Withdraw(sum);
        
        // Assert
        account.Limit.Should().Be(0);
    }
}