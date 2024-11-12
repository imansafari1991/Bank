using Bank.Business.DTOs.BankAccount;
using Bank.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bank.API.Controllers;
[ApiController]
[Route("/api/[controller]")]
public class BankAccountController : ControllerBase
{
    private readonly BankAccountService _bankAccountService;

    public BankAccountController(BankAccountService bankAccountService)
    {
        _bankAccountService = bankAccountService;
    }
    // GET
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        await _bankAccountService.CreateBankAccount(new CreateBankAccountDto() { InitialBalance = 100 });
        return Ok();
    }
    [HttpPost(nameof(Create))]
    public async Task<IActionResult> Create(CreateBankAccountDto dto)
    {
        await _bankAccountService.CreateBankAccount(dto);
        return Ok();
    }
    [HttpPost(nameof(Withdrawal))]
    public async Task<IActionResult> Withdrawal(WithdrawBankAccountDto dto,CancellationToken cancellationToken)
    {
        await _bankAccountService.WithDrawBankAccount(dto, cancellationToken);
        return Ok();
    }
}