using FluentAssertions;
using KataDotNetPossumus.BaseTest.Builders.ApiManager;
using KataDotNetPossumus.BaseTest.Builders.Repository;
using KataDotNetPossumus.BaseTest.Builders.Settings;
using KataDotNetPossumus.Business.Implementations;
using KataDotNetPossumus.Model.DataTransferObject.Wallet;
using KataDotNetPossumus.Model.Entities;
using Xunit;

namespace KataDotNetPossumus.BaseTest.Builders.Business;

public class AccountBusinessTest
{
	#region Test
	
	[Fact]
	public async Task Should_GetAccountsByWallet_Succeed()
	{
		// Arrange
		var accountRepository = new AccountRepositoryBuilder()
			.SetupMethodGetAccountsByWallet(new List<Account>
			{
				new Account
				{
					IdAccount = 1,
					IdWallet = 1,
					IdCurrency = 1,
					Balance = 10,
					Status = 1
				}
			})
			.Build();

		var sut = new AccountBusiness(
			accountRepository,
			null,
			null,
			null,
			null
			);

		// Act
		var act = await sut.GetAccountsByWalletAsync(1);

		// Assert
		act.Should().HaveCount(1);
	}
	
	[Fact]
	public async Task Should_GetAccountsByWalletAndCurrency_Succeed()
	{
		// Arrange
		var accountRepository = new AccountRepositoryBuilder()
			.SetupMethodFindByWalletAndCurrency(new Account
				{
					IdAccount = 1,
					IdWallet = 1,
					IdCurrency = 1,
					Balance = 10,
					Status = 1
				})
			.Build();

		var sut = new AccountBusiness(
			accountRepository,
			null,
			null,
			null,
			null
			);

		// Act
		var act = await sut.GetAccountsByWalletAndCurrencyAsync(1, 1);

		// Assert
		act.IdAccount.Should().Be(1);
	}

	[Fact]
	public async Task Should_Exchange_Succeed()
	{
		// Arrange
		var account = new Account
		{
			IdAccount = 1,
			IdWallet = 1,
			IdCurrency = 1,
			Balance = 10,
			Status = 1
		};

		var accountRepository = new AccountRepositoryBuilder()
			.SetupMethodFindByWalletAndCurrency(account)
			.SetupMethodFind(account)
			.Build();

		var accountHistoryBusiness = new AccountHistoryBusinessBuilder()
			.SetupMethodSaveAccountHistory()
			.Build();

		var contextData = new ContextDataBuilder()
			.SetupUserId(1)
			.Build();

		var currencyApiManager = new CurrencyApiManagerBuilder()
			.SetupMethodGetNewAmount(100)
			.Build();

		var currencyBusiness = new CurrencyBusinessBuilder()
			.SetupMethodByShortName(new Currency())
			.Build();

		var sut = new AccountBusiness(
			accountRepository,
			accountHistoryBusiness,
			contextData,
			currencyApiManager,
			currencyBusiness
			);

		// Act
		await sut.ExchangeAsync(new DtoTransaction
		{
			IdWallet = 1,
			IdAccount = 1,
			IdCurrency = 1,
			IdNewCurrency = 2,
			TransactionAmount = 100
		}, new DtoExchangeTransaction
		{
			CurrentCurrency = "ARS",
			Amount = 100,
			NewCurrency = "USD"
		});
	}

	[Fact]
	public async Task Should_SaveAccount_Succeed()
	{
		// Arrange
		var account = new Account
		{
			IdAccount = 1,
			IdWallet = 1,
			IdCurrency = 1,
			Balance = 10,
			Status = 1
		};

		var accountRepository = new AccountRepositoryBuilder()
			.SetupMethodFindByWalletAndCurrency(account)
			.SetupMethodFind(account)
			.Build();

		var accountHistoryBusiness = new AccountHistoryBusinessBuilder()
			.SetupMethodSaveAccountHistory()
			.Build();

		var contextData = new ContextDataBuilder()
			.SetupUserId(1)
			.Build();

		var currencyApiManager = new CurrencyApiManagerBuilder()
			.SetupMethodGetNewAmount(100)
			.Build();

		var currencyBusiness = new CurrencyBusinessBuilder()
			.SetupMethodByShortName(new Currency())
			.Build();

		var sut = new AccountBusiness(
			accountRepository,
			accountHistoryBusiness,
			contextData,
			currencyApiManager,
			currencyBusiness
			);

		// Act
		await sut.SaveAccountAsync(new DtoTransaction
		{
			IdWallet = 1,
			IdAccount = 1,
			IdCurrency = 1,
			IdNewCurrency = 2,
			TransactionAmount = 100
		});
	}

	#endregion
}