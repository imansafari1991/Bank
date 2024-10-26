namespace Bank.Domain.Entities;
public class BankAccount
{
    public decimal Balance { get;private set; }

    public BankAccount(decimal initialBalance)
    {
        Balance = initialBalance;
    }

}