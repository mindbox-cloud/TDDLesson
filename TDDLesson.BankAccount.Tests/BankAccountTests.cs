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
        Assert.AreEqual(100, bankAccount.Balance);
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
        Assert.AreEqual(200, bankAccount.Balance);
    }
}