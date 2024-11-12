using Bank.Domain.Constants;

namespace Bank.Domain.Entities;

public class BankAccount
{
    public int Id { get; set; }
    public decimal Balance { get;private set; }
    public List<Transaction> Transactions { get; set; }

    public BankAccount(decimal balance)
    {
        Balance = balance;
        Transactions =
        [
            new Transaction(balance)
        ];
    }

    public void Deposit(decimal depositAmount)
    {
        Balance += depositAmount;
        Transactions.Add(new Transaction(depositAmount));
    }

    public void Withdraw(decimal withdrawalAmount)
    {
        if (withdrawalAmount <= 0)
        {
            throw new ArgumentException(BankAccountConstants.InvalidWithdrawRequest);
        }

        if (Balance - withdrawalAmount < 0)
        {
            throw new ArgumentException(BankAccountConstants.InsufficientFunds);
        }

        Balance -= withdrawalAmount;
        Transactions.Add(new Transaction(withdrawalAmount));
    }
}