using Bank.Business.Contracts;
using Bank.Business.DTOs.BankAccount;
using Bank.Domain.Constants;
using Bank.Domain.Entities;

namespace Bank.Business.Services;

public class BankAccountService
{
    private readonly IBankAccountDataService _bankAccountDataService;

    public BankAccountService(IBankAccountDataService bankAccountDataService)
    {
        _bankAccountDataService = bankAccountDataService;
    }

    public async Task CreateBankAccount(CreateBankAccountDto dto)
    {
        var account = new BankAccount(dto.InitialBalance);

        await _bankAccountDataService.AddAsync(account);
    }

    public async Task DepositToBankAccount(DepositBankAccountDto dto, CancellationToken cancellationToken)
    {
        var bankAccount = await GetBankAccountByIdAsync(dto.BankAccountId, cancellationToken);
        bankAccount.Deposit(dto.Amount);
        await _bankAccountDataService.UpdateAsync(bankAccount);
    }

    public async Task WithDrawBankAccount(WithdrawBankAccountDto dto, CancellationToken cancellationToken)
    {
        var bankAccount = await GetBankAccountByIdAsync(dto.BankAccountId, cancellationToken);
        bankAccount.Withdraw(dto.Amount);
        await _bankAccountDataService.UpdateAsync(bankAccount);
    }

    private async Task<BankAccount> GetBankAccountByIdAsync(int id, CancellationToken cancellationToken)
    {
        var bankAccount = await _bankAccountDataService.GetByIdAsync(id, cancellationToken);
        if (bankAccount == null)
        {
            throw new KeyNotFoundException(BankAccountConstants.NotFound);
        }

        return bankAccount;
    }
}