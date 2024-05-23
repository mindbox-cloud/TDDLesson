namespace TDDLesson.BankAccount.Tests;

using FluentAssertions;

[TestClass]
public sealed class BankAccountTests
{
    [DataRow(0, 500, 500)]
    [DataRow(100, 500, 600)]
    [TestMethod]
    public void BankAccount_AddMoney_PositiveBalance(int initialBalance, int amount, int finalBalance)
    {
        var bankAccount = new BankAccount(initialBalance);
        bankAccount.AddMoney(amount);
        bankAccount.Balance.Should().Be(finalBalance);
    }

    [DataRow(1000, 1000, 0)]
    [DataRow(1000, 500, 500)]
    [TestMethod]
    public void BankAccount_WithdrawMoney_AmountReduced(int initialBalance, int amount, int finalBalance)
    {
        var bankAccount = new BankAccount(initialBalance);
        bankAccount.WithdrawMoney(amount);
        bankAccount.Balance.Should().Be(finalBalance);
    }

    [DataRow(0, -100)]
    [DataRow(0, 0)]
    [TestMethod]
    public void AddMoney_NegativeNumber_ThrowInvalidOperationException(int initialBalance, int amount)
    {
        var bankAccount = new BankAccount(initialBalance);
        FluentActions.Invoking(() => bankAccount.AddMoney(amount)).Should().Throw<InvalidOperationException>();
    }

    [DataRow(0, 100)]
    [DataRow(100, 500)]
    [TestMethod]
    public void WithdrawMoney_NotEnoughMoney_ThrowInvalidOperationException(int initialBalance, int amount)
    {
        var bankAccount = new BankAccount(initialBalance);
        FluentActions.Invoking(() => bankAccount.WithdrawMoney(amount)).Should().Throw<InvalidOperationException>();
    }

    [DataRow(1000, -100)]
    [DataRow(1000, 0)]
    [TestMethod]
    public void WithdrawMoney_NegativeNumber_ThrowInvalidOperationException(int initialBalance, int amount)
    {
        var bankAccount = new BankAccount(initialBalance);
        FluentActions.Invoking(() => bankAccount.WithdrawMoney(amount)).Should().Throw<InvalidOperationException>();
    }
}