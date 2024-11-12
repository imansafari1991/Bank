using Bank.Domain.Constants;
using FluentAssertions;

namespace Bank.Domain.Tests.Unit.BankAccount;

public class FailureBankAccountTests
{
    [Fact]
    public void Should_ThrowException_When_InsufficientFundsForWithdrawal()
    {
        //Arrange
        var initialBalance = 100m;
        var account = new Entities.BankAccount(initialBalance);
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
        var account = new Entities.BankAccount(200m);

        // Act
        Action act = () => account.Withdraw(-10m);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage(BankAccountConstants.InvalidWithdrawRequest);
    }
  
}