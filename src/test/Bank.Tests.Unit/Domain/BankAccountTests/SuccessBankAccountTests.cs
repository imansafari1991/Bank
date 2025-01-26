using Bank.Domain.Entities;
using FluentAssertions;

namespace Bank.Tests.Unit.Domain.BankAccountTests;

public class SuccessBankAccountTests
{
    [Fact]
    public void Should_CreateAccount_With_InitialBalance()
    {
        //Arrange
        var initialBalance = 100m;
        //Act
        var account = BankAccount.CreateAccount(initialBalance);
        //Assert 
        account.Balance.Should().Be(initialBalance);
    }

    [Fact]
    public void Should_IncreaseBalance_When_DepositIsMade()
    {
        //Arrange
        var initialBalance = 100m;
        var account = BankAccount.CreateAccount(initialBalance);
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
        var account = BankAccount.CreateAccount(initialBalance);
        var withdrawalAmount = 50m;
        //Act
        account.Withdraw(withdrawalAmount);
        //Assert
        account.Balance.Should().Be(initialBalance - withdrawalAmount);
    }

    [Fact]
    void Should_AddDebitTransaction_WhenWithdrawIsMade()
    {
        //arrange 
        var initialBalance = 100m;
        var account = BankAccount.CreateAccount(initialBalance);
        var withdrawalAmount = 50m;
        //act
        account.Withdraw(withdrawalAmount);
        //assert
        var withdrawTransaction = account.Transactions.FirstOrDefault(p => p.Debit == withdrawalAmount);
        withdrawTransaction.Should().NotBeNull();
        withdrawTransaction?.Debit.Should().Be(withdrawalAmount);
    }
    [Fact]
    void Should_AddCreditTransaction_WhenDepositIsMade()
    {
        //arrange 
        var initialBalance = 100m;
        var account = BankAccount.CreateAccount(initialBalance);
        var depositAmount = 50m;
        //act
        account.Deposit(depositAmount);
        //assert
        var depositTransaction = account.Transactions.FirstOrDefault(p => p.Credit == depositAmount);
        depositTransaction.Should().NotBeNull();
        depositTransaction?.Credit.Should().Be(depositAmount);
    }

    [Fact]
    void After_Some_Transaction_Balance_Should_Be_Equal_To_Difference_Between_Credits_And_Debit_Transactions()
    {
        //arrange 
        var initialBalance = 100m;
        var account = BankAccount.CreateAccount(initialBalance);
        var depositAmount = 150m;
        var withdrawalAmount = 200m;  
        //act
        account.Deposit(depositAmount);
        account.Withdraw(withdrawalAmount);
        //assert
        var transactionBalance= account.Transactions.Sum(p=>p.Credit-p.Debit);
        transactionBalance.Should().Be(account.Balance);
        
    }
}