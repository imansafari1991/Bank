using Bank.Business.Contracts;
using Bank.Domain.Entities;
using Bank.Persistence.DataServices;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Bank.Persistence.Tests.Integration.BankAccount;

public class BankAccountTests : IDisposable
{
    private DbContextOptions<BankDbContext> _dbContextOptions;

    public BankAccountTests()
    {
        _dbContextOptions = new DbContextOptionsBuilder<BankDbContext>()
            .UseInMemoryDatabase(databaseName: "BankTestDb")
            .Options;
    }

    [Fact]
    public async Task Should_Save_BankAccount()
    {
        var bankAccount = new Domain.Entities.BankAccount(100);
        await using var context = new BankDbContext(_dbContextOptions);
        var bankAccountDataService = new BankAccountDataService(context);
        await bankAccountDataService.AddAsync(bankAccount);

        var bankAccounts = await context.BankAccounts.ToListAsync();
        var savedBankAccount = bankAccounts.Should().ContainSingle().Which;

        savedBankAccount.Id.Should().Be(bankAccount.Id);
        savedBankAccount.Balance.Should().Be(bankAccount.Balance);
    }

    [Fact]
    public async Task Should_Update_BankAccount_Balance_After_Deposit()
    {
        // arrange
        var bankAccount = new Domain.Entities.BankAccount(100);
        await using var context = new BankDbContext(_dbContextOptions);
        context.BankAccounts.Add(bankAccount);
        await context.SaveChangesAsync();
        var savedBankAccount = await context.BankAccounts.FirstAsync();
        //Act
        savedBankAccount.Deposit(50);
        var bankAccountDataService = new BankAccountDataService(context);
        await bankAccountDataService.UpdateAsync(savedBankAccount);
        //Assert
        var bankAccounts = await context.BankAccounts.ToListAsync();
        var updatedBankAccount = bankAccounts.Should().ContainSingle().Which;
        updatedBankAccount.Balance.Should().Be(150);

    }
    [Fact]
    public async Task Should_Return_Correct_BankAccount_By_Id()
    {
        // arrange
        var bankAccount = new Domain.Entities.BankAccount(100);
        await using var context = new BankDbContext(_dbContextOptions);
        var bankAccountDataService = new BankAccountDataService(context);
        var entity = context.BankAccounts.Add(bankAccount);
        await context.SaveChangesAsync();

        // act
        var bankAccountRes = await bankAccountDataService.GetByIdAsync(entity.Entity.Id, default);

        // assert
        bankAccountRes.Should().NotBeNull();
        bankAccountRes?.Balance.Should().Be(100);
        bankAccountRes?.Id.Should().Be(entity.Entity.Id);
    }
    
    public void Dispose()
    {
        var context = new BankDbContext(_dbContextOptions);
        context.Database.EnsureDeleted();
    }
}