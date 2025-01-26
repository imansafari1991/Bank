using Bank.Domain.Entities;

namespace Bank.Business.Contracts;

public interface IBankAccountDataService
{
    Task<BankAccount?> GetByIdAsync(int id,CancellationToken cancellationToken);
    Task<BankAccount> AddAsync(BankAccount account);
    Task UpdateAsync(BankAccount account);
}