using Bank.Persistence;
using Bank.Persistence.DataServices;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Bank.Integration.Test.Persistence.BankAccount;

public class BankAccountTests : IDisposable
{
    private readonly BankDbContext _dbContext;
    private readonly BankAccountDataService _sut;

    public BankAccountTests()
    {
        var dbContextOptions = new DbContextOptionsBuilder<BankDbContext>()
            .UseInMemoryDatabase(databaseName: $"BankTestDb")
            .Options;
        _dbContext = new BankDbContext(dbContextOptions);
        _sut = new BankAccountDataService(_dbContext);
    }

    [Fact]
    public async Task Should_Save_BankAccount()
    {
        //arrange 
        var initialBalance = 100m;
        var bankAccount = Domain.Entities.BankAccount.CreateAccount(initialBalance);

        //act
        var savedBankAccount = await _sut.AddAsync(bankAccount);

        //assert
        var actualBankAccount = await _dbContext.BankAccounts.FirstOrDefaultAsync(p => p.Id == savedBankAccount.Id);
        actualBankAccount.Should().NotBeNull();
        actualBankAccount?.Id.Should().Be(bankAccount.Id);
        actualBankAccount?.Balance.Should().Be(bankAccount.Balance);
    }

    [Fact]
    public async Task Should_Update_BankAccount_Balance_After_Deposit()
    {
        // arrange
        var initialBalance = 100m;
        var bankAccount = Domain.Entities.BankAccount.CreateAccount(initialBalance);
        var savedBankAccount = await _sut.AddAsync(bankAccount);
        //Act
        savedBankAccount.Deposit(50);
        await _sut.UpdateAsync(savedBankAccount);
        //Assert
        var actualBankAccount = await _dbContext.BankAccounts.FirstOrDefaultAsync(p => p.Id == savedBankAccount.Id);
        actualBankAccount?.Balance.Should().Be(150);
    }

    [Fact]
    public async Task Should_Update_BankAccount_Balance_After_Withdrawal()
    {
        // arrange
        var initialBalance = 100m;
        var bankAccount = Domain.Entities.BankAccount.CreateAccount(initialBalance);
        var savedBankAccount = await _sut.AddAsync(bankAccount);
        //Act
        savedBankAccount.Withdraw(50);
        await _sut.UpdateAsync(savedBankAccount);
        //Assert
        var actualBankAccount = await _dbContext.BankAccounts.FirstOrDefaultAsync(p => p.Id == savedBankAccount.Id);
        actualBankAccount?.Balance.Should().Be(50);
    }

    public void Dispose()
    {
        _dbContext.Database.EnsureDeleted();
    }
}