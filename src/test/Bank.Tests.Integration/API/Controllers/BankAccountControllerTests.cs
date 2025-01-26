using System.Net;
using System.Text;
using System.Text.Json;
using Bank.Business.DTOs.BankAccount;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Bank.Tests.Integration.API.Controllers;

public class BankAccountControllerTests : IAsyncDisposable
{
    private readonly WebApplicationFactory<Program> _webApplicationFactory;
    private readonly HttpClient _httpClient;
    public BankAccountControllerTests()
    {
        _webApplicationFactory = new WebApplicationFactory<Program>();
        _httpClient = _webApplicationFactory.CreateClient(new WebApplicationFactoryClientOptions() { BaseAddress = new Uri("http://localhost:5262/") });
    }
    public ValueTask DisposeAsync()
    {
        return ((IAsyncDisposable)_webApplicationFactory).DisposeAsync();
    }
    [Fact]
    public async Task Should_respond_a_status_200_OK()
    {
        // Act
        var result = await _httpClient.GetAsync("/api/BankAccount");

        // Assert
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public async Task Should_respond_a_status_404_when_account_not_found()
    {
        // Act
        var data = new WithdrawBankAccountDto { BankAccountId = -1, Amount = 20 };
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var result = await _httpClient.PostAsync("/api/BankAccount/WithDrawal",content);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }
    
}