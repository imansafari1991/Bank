namespace Bank.Domain.Constants;

public static class BankAccountConstants
{
    public const string InsufficientFunds = "Insufficient funds for this bank.";
    public const string InvalidWithdrawRequest = "Withdraw request cannot be less than or equal to zero.";
    public static string NotFound ="Bank Account Not Found";
}