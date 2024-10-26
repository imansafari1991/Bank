using Bank.Domain.Entities;
using FluentAssertions;

namespace Bank.Domain.Tests.Unit.Account;

public class AccountTests
{
    [Fact]
    public void Should_CreateAccount_With_InitialBalance()
    {
        //Arrange
        var initialBalance = 100m;
        //Act
        var account=new BankAccount(initialBalance);
        //Assert 
        account.Balance.Should().Be(initialBalance);
    }
    
}

