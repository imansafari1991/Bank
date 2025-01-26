using Bank.Domain.Constants;

namespace Bank.Domain.Entities;

public class BankAccount
{
    public int Id { get; set; }
    public decimal Balance { get; private set; }
    public List<Transaction> Transactions { get; private init; }


    private BankAccount()
    {
    }

    public static BankAccount CreateAccount(decimal initialBalance)
    {
        return new BankAccount()
        {
            Balance = initialBalance,
            Transactions = [Transaction.CreateCredit(initialBalance)]
        };
    }

    public void Deposit(decimal depositAmount)
    {
        Balance += depositAmount;
        AddCreditTransaction(depositAmount);
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
        AddDebitTransaction(withdrawalAmount);
    }

    private void AddDebitTransaction(decimal withdrawalAmount)
    {
        Transactions.Add(Transaction.CreateDebit(withdrawalAmount));
    }

    private void AddCreditTransaction(decimal depositAmount)
    {
        Transactions.Add(Transaction.CreateCredit(depositAmount));
    }
}