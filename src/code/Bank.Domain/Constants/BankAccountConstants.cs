namespace Bank.Domain.Constants;

public static class BankAccountConstants
{
    public const string InsufficientFunds = "Insufficient funds for this bank account. ";
    public const string InvalidWithdrawRequest = "Withdraw request cannot be less than or equal to zero.";
    public const string NotFound ="Bank Account Not Found";
}