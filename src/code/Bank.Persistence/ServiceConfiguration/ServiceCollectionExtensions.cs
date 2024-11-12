using Bank.Business.Contracts;
using Bank.Persistence.DataServices;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Persistence.ServiceConfiguration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
    {
        var conn = CreateDbConnection();
        services
            .AddDbContext<BankDbContext>(options => options.UseSqlite(conn));
        EnsureDatabaseCreated(conn);

        services.AddScoped<IBankAccountDataService, BankAccountDataService>();
        return services;
    }
    private static SqliteConnection CreateDbConnection()
    {
        var connString = "Data Source=bank.db";
        var conn = new SqliteConnection(connString);
        conn.Open();
        return conn;
    }
    private static void EnsureDatabaseCreated(SqliteConnection conn)
    {
        var builder = new DbContextOptionsBuilder<BankDbContext>();
        builder.UseSqlite(conn);
        using var context=new BankDbContext(builder.Options);
        context.Database.EnsureCreated();
        //context.Database.Migrate();
    }
}