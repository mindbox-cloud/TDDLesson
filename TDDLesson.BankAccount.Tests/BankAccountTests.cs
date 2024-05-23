namespace TDDLesson.BankAccount.Tests;

using FluentAssertions;

[TestClass]
public sealed class BankAccountTests
{
    [TestMethod]
    public void AddMoneyToBankAccount_MoneySuccessfullyAdded()
    {
        // Arrange
        var bankAccount = new BankAccount();
        var money = 100;

        // Act
        bankAccount.Add(money);

        // Assert
        bankAccount.Balance.Should().Be(100);
    }
    
    [TestMethod]
    public void AddMoneyToNonEmptyBankAccount_MoneySuccessfullyAddedToExistingBalance()
    {
        // Arrange
        var bankAccount = new BankAccount();
        var money = 100;

        // Act
        bankAccount.Add(money);
        bankAccount.Add(money);

        // Assert
        bankAccount.Balance.Should().Be(200);
    }
    
    [TestMethod]
    public void AddMoneyToNonEmptyBankAccount_NegativeMoney_ThrowInvalidOperationException()
    {
        // Arrange
        var bankAccount = new BankAccount();
        var money = -100;

        // Act
        var act = () => bankAccount.Add(money);

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }
    
    [TestMethod]
    public void TakeAllMoneyFromBankAccount_MoneySuccessfullyTaken()
    {
        // Arrange
        var bankAccount = new BankAccount();
        var money = 100;
        bankAccount.Add(money);
        
        // Act
        bankAccount.Take(money);

        // Assert
        bankAccount.Balance.Should().Be(0);
    }
    
    [TestMethod]
    public void TakeSomeMoneyFromBankAccount_MoneySuccessfullyTaken()
    {
        // Arrange
        var bankAccount = new BankAccount();
        bankAccount.Add(100);
        
        // Act
        bankAccount.Take(50);

        // Assert
        bankAccount.Balance.Should().Be(50);
    }
    
    [TestMethod]
    public void TakeMoreMoneyThenHaveFromBankAccount_Error_MoneyNotEnough()
    {
        // Arrange
        var bankAccount = new BankAccount();
        bankAccount.Add(50);
        
        // Act
        var act = () => bankAccount.Take(100);

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }
    
    [TestMethod]
    public void TakeMoneyFromBankAccount_NegativeMoney_ThrowInvalidOperationException()
    {
        // Arrange
        var bankAccount = new BankAccount();
        var money = -100;

        // Act
        var act = () => bankAccount.Take(money);

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }
    
    [TestMethod]
    public void CreateBankAccount_WithoutLimit_LimitIsZero()
    {
        // Act
        var bankAccount = new BankAccount();

        // Assert
        bankAccount.Limit.Should().Be(0);
    }
    
    [TestMethod]
    public void CreateBankAccount_WithLimit_LimitSetSuccessfully()
    {
        // Arrange
        var limit = 1000;
        
        // Act
        var bankAccount = new BankAccount(limit);

        // Assert
        bankAccount.Limit.Should().Be(limit);
    }
    
    [TestMethod]
    public void CreateBankAccount_WithNegativeLimit_ThrowInvalidOperationException()
    {
        // Arrange
        var limit = -1000;
        
        // Act
        var act = () => new BankAccount(limit);
        
        // Assert
        act.Should().Throw<InvalidOperationException>();
    }
}