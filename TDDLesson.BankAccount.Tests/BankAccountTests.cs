﻿namespace TDDLesson.BankAccount.Tests;

using FluentAssertions;

[TestClass]
public sealed class BankAccountTests
{
    [TestMethod]
    public void BalanceZero_WhenBankAccountCreated()
    {
        var account = new BankAccount();

        account.Balance.Should().Be(0);
    }
    
    [TestMethod]
    public void AddMoney_WhenMoneyIsGraterThanZero()
    {
        var account = new BankAccount();
        var money = 1;
        
        account.Add(money);

        account.Balance.Should().Be(money);
    }
    
    [TestMethod]
    public void FailAdd_WhenMoneyIsLessThanZero()
    {
        var account = new BankAccount();
        var money = 0;
        
        var act = () => account.Add(money);

        act.Should().Throw<InvalidOperationException>().WithMessage("Money should be more zero");
    }
    
    [TestMethod]
    public void TakeMoney_WhenBalanceSufficient()
    {
        var account = new BankAccount(1);
        var money = 1;
        
        account.Take(money);

        account.Balance.Should().Be(0);
    }
    
    [TestMethod]
    public void FailTakeMoney_WhenMoneyIsNegative()
    {
        var account = new BankAccount(1);
        var money = -1;
        
        var act = () => account.Take(money);

        act.Should().Throw<InvalidOperationException>().WithMessage("Money should be more zero");
    }
    
    [TestMethod]
    public void FailTakeMoney_WhenMoneyInsufficient()
    {
        var account = new BankAccount(1);
        var money = 2;
        
        var act = () => account.Take(money);

        act.Should().Throw<InvalidOperationException>();
    }
}