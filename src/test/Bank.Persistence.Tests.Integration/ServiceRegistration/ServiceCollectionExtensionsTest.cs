using Bank.Persistence.ServiceConfiguration;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Persistence.Tests.Integration.ServiceRegistration;

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