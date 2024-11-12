namespace Bank.Business.DTOs.BankAccount;

public class WithdrawBankAccountDto
{
    public int BankAccountId { get; init; }
    public decimal Amount { get; init; }
}