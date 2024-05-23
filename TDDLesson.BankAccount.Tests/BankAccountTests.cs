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
        bankAccount.Should().Be(100);
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
        bankAccount.Should().Be(200);
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
}