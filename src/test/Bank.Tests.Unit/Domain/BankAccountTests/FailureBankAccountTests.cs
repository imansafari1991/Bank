using Bank.Domain.Constants;
using FluentAssertions;

namespace Bank.Tests.Unit.Domain.BankAccountTests;

public class FailureBankAccountTests
{
    [Fact]
    public void Should_ThrowException_When_InsufficientFundsForWithdrawal()
    {
        //Arrange
        var initialBalance = 100m;
        var account = Bank.Domain.Entities.BankAccount.CreateAccount(initialBalance);
        var withdrawalAmount = 150m;
        //Act
        Action act = () => account.Withdraw(withdrawalAmount);
        //Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage(BankAccountConstants.InsufficientFunds);
    }
    [Fact]
    public void Should_ThrowException_When_WithdrawingNegativeAmount()
    {
        // Arrange
        var account = Bank.Domain.Entities.BankAccount.CreateAccount(200m);

        // Act
        Action act = () => account.Withdraw(-10m);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage(BankAccountConstants.InvalidWithdrawRequest);
    }
  
}