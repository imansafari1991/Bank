using Bank.Business.Contracts;
using Bank.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bank.Persistence.DataServices;

public class BankAccountDataService : IBankAccountDataService
{
    private readonly BankDbContext _context;
    public BankAccountDataService(BankDbContext context)
    {
        _context = context;
    }

    public async Task<BankAccount?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.BankAccounts.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<BankAccount> AddAsync(BankAccount account)
    {
        _context.Add(account);
        await _context.SaveChangesAsync();
        return account;
    }

    public async Task UpdateAsync(BankAccount account)
    {
        _context.Update(account);
        await _context.SaveChangesAsync();
    }
}