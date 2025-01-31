using Bank.Persistence;
using Bank.Persistence.ServiceConfiguration;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Tests.Integration.Persistence.ServiceRegistration;

public class ServiceCollectionExtensionsTest
{
    [Fact]
    public void Should_Register_DbContext()
    {
        // Arrange
        var services = new ServiceCollection();
        
        // Act
        services.AddPersistenceServices();
        var serviceProvider = services.BuildServiceProvider();
        
        //Assert
        serviceProvider.GetService<BankDbContext>().Should().NotBeNull();
    }
}