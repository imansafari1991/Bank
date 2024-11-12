namespace Bank.Domain.Entities;

public class Transaction
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get;private set; }
    public Transaction(decimal amount)
    {
        Amount = amount;
        Date = DateTime.Now;
    }
    public int BankAccountId { get; init; }
    public BankAccount BankAccount { get; init; }
}