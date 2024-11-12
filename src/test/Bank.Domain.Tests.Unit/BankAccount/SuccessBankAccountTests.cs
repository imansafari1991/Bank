using Bank.Domain.Constants;
using FluentAssertions;

namespace Bank.Domain.Tests.Unit.BankAccount;

public class SuccessBankAccountTests
{
    [Fact]
    public void Should_CreateAccount_With_InitialBalance()
    {
        //Arrange
        var initialBalance = 100m;
        //Act
        var account = new Entities.BankAccount(initialBalance);
        //Assert 
        account.Balance.Should().Be(initialBalance);
    }

    [Fact]
    public void Should_IncreaseBalance_When_DepositIsMade()
    {
        //Arrange
        var initialBalance = 100m;
        var account = new Entities.BankAccount(initialBalance);
        var depositAmount = 50m;
        //Act
        account.Deposit(depositAmount);
        //Assert 
        account.Balance.Should().Be(initialBalance + depositAmount);
    }

    [Fact]
    public void Should_DecreaseBalance_When_WithdrawIsMade()
    {
        //Arrange
        var initialBalance = 100m;
        var account = new Entities.BankAccount(initialBalance);
        var withdrawalAmount = 50m;
        //Act
        account.Withdraw(withdrawalAmount);
        //Assert
        account.Balance.Should().Be(initialBalance - withdrawalAmount);
    }
    
}