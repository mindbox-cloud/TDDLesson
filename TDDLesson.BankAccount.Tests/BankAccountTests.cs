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

    [DataRow(10, 10, 20, -10)]
    [DataRow(10, 20, 25, -15)]
    [TestMethod]
    public void WithdrawMoney_EnoughLimit_NegativeAmount(int initialBalance, int limit, int amount, int finalBalance)
    {
        var bankAccount = new BankAccount(initialBalance, limit);
        bankAccount.WithdrawMoney(amount);
        bankAccount.Balance.Should().Be(finalBalance);
    }
    
    [DataRow(10, 10, 21)]
    [DataRow(10, 20, 2000)]
    [TestMethod]
    public void WithdrawMoney_NotEnoughLimit_ThrowInvalidOperationException(int initialBalance, int limit, int amount)
    {
        var bankAccount = new BankAccount(initialBalance, limit);
        FluentActions.Invoking(() => bankAccount.WithdrawMoney(amount)).Should().Throw<InvalidOperationException>();
    }

    [TestMethod]
    public void CreateBalance_NegativeLimit_ThrowArgumentException()
    {
        FluentActions.Invoking(() => new BankAccount(100, limit: -100)).Should().Throw<ArgumentException>();
    }
}