namespace Bank.Domain.Entities;

public class Transaction
{
    public int Id { get; set; }
    public decimal Debit { get; set; }
    public decimal Credit { get; set; }
    public DateTime Date { get;private set; }

    private Transaction()
    {
        
    }
    public static Transaction CreateCredit(decimal credit)
    {
        return new Transaction()
        {
            Credit = credit,
            Date = DateTime.UtcNow
        };
    }
    public static Transaction CreateDebit(decimal debit)
    {
        return new Transaction()
        {
            Debit = debit,
            Date = DateTime.UtcNow
        };
    }
    public int BankAccountId { get; init; }
    public BankAccount BankAccount { get; init; }
}