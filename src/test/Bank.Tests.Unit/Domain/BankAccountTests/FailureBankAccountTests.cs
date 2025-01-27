using Bank.Domain.Constants;
using Bank.Domain.Entities;
using FluentAssertions;
using Shouldly;

namespace Bank.Tests.Unit.Domain.BankAccountTests;

public class FailureBankAccountTests
{
    [Fact]
    public void Should_ThrowException_When_InsufficientFundsForWithdrawal()
    {
        //Arrange
        var initialBalance = 100m;
        var account = BankAccount.CreateAccount(initialBalance);
        var withdrawalAmount = 150m;
        //Act
        Action act = () => account.Withdraw(withdrawalAmount);
        //Assert
        //Fluent Assertion
        act.Should().Throw<ArgumentException>()
            .WithMessage(BankAccountConstants.InvalidWithdrawRequest);
        //Or
        //Shouldly
        act.ShouldThrow<ArgumentException>()
            .Message.ShouldBe(BankAccountConstants.InsufficientFunds);
    }
    [Fact]
    public void Should_ThrowException_When_WithdrawingNegativeAmount()
    {
        // Arrange
        var account = BankAccount.CreateAccount(200m);

        // Act
        Action act = () => account.Withdraw(-10m);

        // Assert
        //Fluent Assertion
        act.Should().Throw<ArgumentException>()
            .WithMessage(BankAccountConstants.InsufficientFunds);
        //Shouldly
        act.ShouldThrow<ArgumentException>()
            .Message.ShouldBe(BankAccountConstants.InvalidWithdrawRequest);
        
    }
  
}