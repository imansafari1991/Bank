using Bank.Business.Contracts;
using Bank.Business.DTOs.BankAccount;
using Bank.Business.Services;
using Bank.Domain.Constants;
using Bank.Domain.Entities;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Bank.Tests.Unit.Business.BankAccountServiceTests
{
    public class BankAccountServiceTests
    {
        private readonly BankAccountService _sut;
        private readonly IBankAccountDataService _bankAccountDataService;
        private const decimal InitialBalance = 100.00M;

        public BankAccountServiceTests()
        {
            //Arrange
            _bankAccountDataService = Substitute.For<IBankAccountDataService>();
            _bankAccountDataService.GetByIdAsync(1, default).Returns(BankAccount.CreateAccount(InitialBalance));

            _sut = new BankAccountService(_bankAccountDataService);
        }

        [Fact]
        public async Task Should_Call_Add_Method_After_Create_New_Account()
        {
            //Act
            await _sut.CreateBankAccount(new CreateBankAccountDto());
            //Assert
            await _bankAccountDataService.Received(1).AddAsync(Arg.Any<BankAccount>());
        }

        [Fact]
        public async Task Should_Call_Add_Method_With_Same_Initial_Balance_Create_New_BankAccount()
        {
            //Act
            await _sut.CreateBankAccount(new CreateBankAccountDto() { InitialBalance = InitialBalance });
            //Assert
            await _bankAccountDataService.Received(1).AddAsync(Arg.Is<BankAccount>(x => x.Balance == InitialBalance));
        }

        [Fact]
        public async Task Should_Call_Update_Method_After_Deposit_With_New_Balance_After_Update_BankAccount()
        {
            await _sut.DepositToBankAccount(new DepositBankAccountDto() { BankAccountId = 1, Amount = 50 }, default);

            //Assert
            await _bankAccountDataService.Received(1).UpdateAsync(Arg.Is<BankAccount>(x => x.Balance == InitialBalance + 50));
        }

        [Fact]
        public async Task Should_Call_Update_Method_After_Withdrawal_With_New_Balance_After_Update_BankAccount()
        {
            await _sut.WithDrawBankAccount(new WithdrawBankAccountDto() { BankAccountId = 1, Amount = 50 }, default);

            //Assert
            await _bankAccountDataService.Received(1).UpdateAsync(Arg.Is<BankAccount>(x => x.Balance == InitialBalance - 50));
        }

        [Fact]
        public async Task Should_Throw_Not_Found_Exception_When_Account_Not_FoundAsync()
        {
            //Arrange
            _bankAccountDataService.GetByIdAsync(-1, default).ReturnsNull();

            // Act
            Func<Task> act = async () => await _sut.WithDrawBankAccount(new WithdrawBankAccountDto() { BankAccountId = -1 }, default);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage(BankAccountConstants.NotFound);
        }
    }
}