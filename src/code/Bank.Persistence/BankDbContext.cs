using Bank.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bank.Persistence;

public class BankDbContext : DbContext
{
    public BankDbContext(DbContextOptions<BankDbContext> options) : base(options)
    {
    }

    public DbSet<BankAccount> BankAccounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<BankAccount>(b =>
    //     {
    //         b.HasKey(e => e.Id);
    //         b.Property(e => e.Id).ValueGeneratedOnAdd();
    //         b.Property(e => e.Balance);
    //     });
    //     modelBuilder.Entity<Transaction>().HasKey(t => t.Id);
    //     modelBuilder.Entity<BankAccount>()
    //         .HasMany(b => b.Transactions)
    //         .WithOne()
    //         .HasForeignKey("BankAccountId");
    //     base.OnModelCreating(modelBuilder);
    // }
}