using FluentAssertions;
using KataDotNetPossumus.BaseTest.Builders.Repository;
using KataDotNetPossumus.Business.Implementations;
using KataDotNetPossumus.Exceptions;
using KataDotNetPossumus.Model.DataTransferObject.Wallet;
using KataDotNetPossumus.Model.Entities;
using KataDotNetPossumus.Resources;
using Xunit;

namespace KataDotNetPossumus.BaseTest.Builders.Business;

public class WalletBusinessTest
{
	[Fact]
	public async Task Should_GetBalanceByUser_Fail_WalletNotFound()
	{
		// Arrange
		var accountBusiness = new AccountBusinessBuilder()
			.SetupMethodGetAccountsByWallet(null)
			.Build();
		var walletRepository = new WalletRepositoryBuilder()
			.SetupMethodGetWalletByUser(null)
			.Build();

		var sut = new WalletBusiness(
			accountBusiness,
			null,
			walletRepository);

		// Act
		Task Act() => sut.GetBalanceByUserAsync();

		// Assert
		var exception = await Assert.ThrowsAsync<NotFoundException>(Act);

		exception.Message.Should().Be(string.Format(Messages.EntityNotFoundFormat, Labels.Wallet));
	}

	[Fact]
	public async Task Should_GetBalanceByUser_Fail_AccountsNotFound()
	{
		// Arrange
		var accountBusiness = new AccountBusinessBuilder()
			.SetupMethodGetAccountsByWallet(null)
			.Build();

		var walletRepository = new WalletRepositoryBuilder()
			.SetupMethodGetWalletByUser(new Wallet())
			.Build();

		var sut = new WalletBusiness(
			accountBusiness,
			null,
			walletRepository);

		// Act
		Task Act() => sut.GetBalanceByUserAsync();

		// Assert
		var exception = await Assert.ThrowsAsync<NotFoundException>(Act);

		exception.Message.Should().Be(string.Format(Messages.EntityNotFoundFormat, Labels.Account));
	}

	[Fact]
	public async Task Should_GetBalanceByUser_Succeed()
	{
		// Arrange
		var accountBusiness = new AccountBusinessBuilder()
			.SetupMethodGetAccountsByWallet(new List<Account>
			{
				new Account
				{
					IdAccount = 1,
					IdWallet = 1,
					IdCurrency = 1,
					Balance = 10,
					Status = 1
				},
				new Account
				{
					IdAccount = 2,
					IdWallet = 1,
					IdCurrency = 2,
					Balance = 100,
					Status = 1
				}
			})
			.Build();

		var walletRepository = new WalletRepositoryBuilder()
			.SetupMethodGetWalletByUser(new Wallet())
			.Build();

		var sut = new WalletBusiness(
			accountBusiness,
			null,
			walletRepository);

		// Act
		var act = await sut.GetBalanceByUserAsync();

		// Assert
		act?.AccountsBalances.Should().HaveCount(2);
	}

	[Fact]
	public async Task Should_Deposit_Succeed()
	{
		// Arrange
		var accountBusiness = new AccountBusinessBuilder()
			.SetupMethodGetAccountsByWallet(new List<Account>
			{
				new Account
				{
					IdAccount = 1,
					IdWallet = 1,
					IdCurrency = 1,
					Balance = 10,
					Status = 1
				},
				new Account
				{
					IdAccount = 2,
					IdWallet = 1,
					IdCurrency = 2,
					Balance = 100,
					Status = 1
				}
			})
			.Build();

		var currencyBusiness = new CurrencyBusinessBuilder()
			.SetupMethodByShortName(new Currency
			{
				IdCurrency = 1,
				ShortName = "ARS",
				Status = 1
			})
			.Build();

		var walletRepository = new WalletRepositoryBuilder()
			.SetupMethodGetWalletByUser(new Wallet())
			.Build();

		var sut = new WalletBusiness(
			accountBusiness,
			currencyBusiness,
			walletRepository);

		// Act
		await sut.DepositAsync(new DtoTransactionRequest
		{
			CurrentCurrency = "USD",
			Amount = 12
		});
	}
	
	[Fact]
	public async Task Should_Withdraw_Succeed()
	{
		// Arrange
		var accountBusiness = new AccountBusinessBuilder()
			.SetupMethodGetAccountsByWallet(new List<Account>
			{
				new Account
				{
					IdAccount = 1,
					IdWallet = 1,
					IdCurrency = 1,
					Balance = 10,
					Status = 1
				},
				new Account
				{
					IdAccount = 2,
					IdWallet = 1,
					IdCurrency = 2,
					Balance = 100,
					Status = 1
				}
			})
			.SetupMethodAccountsByWalletAndCurrency(new Account
			{
				IdAccount = 2,
				IdWallet = 1,
				IdCurrency = 2,
				Balance = 100,
				Status = 1
			})
			.Build();

		var currencyBusiness = new CurrencyBusinessBuilder()
			.SetupMethodByShortName(new Currency
			{
				IdCurrency = 1,
				ShortName = "ARS",
				Status = 1
			})
			.Build();

		var walletRepository = new WalletRepositoryBuilder()
			.SetupMethodGetWalletByUser(new Wallet())
			.Build();

		var sut = new WalletBusiness(
			accountBusiness,
			currencyBusiness,
			walletRepository);

		// Act
		await sut.WithdrawAsync(new DtoTransactionRequest
		{
			CurrentCurrency = "USD",
			Amount = 12
		});
	}

	[Fact]
	public async Task Should_Exchange_Succeed()
	{
		// Arrange
		var accountBusiness = new AccountBusinessBuilder()
			.SetupMethodGetAccountsByWallet(new List<Account>
			{
				new Account
				{
					IdAccount = 1,
					IdWallet = 1,
					IdCurrency = 1,
					Balance = 10,
					Status = 1
				},
				new Account
				{
					IdAccount = 2,
					IdWallet = 1,
					IdCurrency = 2,
					Balance = 100,
					Status = 1
				}
			})
			.SetupMethodAccountsByWalletAndCurrency(new Account
			{
				IdAccount = 2,
				IdWallet = 1,
				IdCurrency = 2,
				Balance = 100,
				Status = 1
			})
			.Build();

		var currencyBusiness = new CurrencyBusinessBuilder()
			.SetupMethodByShortName(new Currency
			{
				IdCurrency = 1,
				ShortName = "ARS",
				Status = 1
			})
			.Build();

		var walletRepository = new WalletRepositoryBuilder()
			.SetupMethodGetWalletByUser(new Wallet())
			.Build();

		var sut = new WalletBusiness(
			accountBusiness,
			currencyBusiness,
			walletRepository);

		// Act
		await sut.ExchangeAsync(new DtoExchangeTransaction()
		{
			CurrentCurrency = "USD",
			Amount = 12,
			NewCurrency = "ARS"
		});
	}
	
	[Fact]
	public async Task Should_Deposit_Fail_RequestIsNull()
	{
		// Arrange
		var sut = new WalletBusiness(
			null,
			null,
			null);

		// Act
		Task Act() => sut.DepositAsync(null);
		
		// Assert
		var exception = await Assert.ThrowsAsync<BadRequestException>(Act);

		exception.Message.Should().Be("request");
	}

	[Fact]
	public async Task Should_Deposit_Fail_AmountIsRequired()
	{
		// Arrange
		var sut = new WalletBusiness(
			null,
			null,
			null);

		// Act
		Task Act() => sut.DepositAsync(new DtoTransactionRequest
		{
			CurrentCurrency = "USD",
			Amount = null
		});

		// Assert
		var exception = await Assert.ThrowsAsync<RequiredDataException>(Act);

		exception.Message.Should().Be(string.Format(Messages.RequiredFieldFormat, Labels.Amount));
	}

	[Fact]
	public async Task Should_Exchange_Fail_NewCurrencyIsRequired()
	{
		// Arrange
		var sut = new WalletBusiness(
			null,
			null,
			null);

		// Act
		Task Act() => sut.ExchangeAsync(new DtoExchangeTransaction
		{
			CurrentCurrency = "USD",
			Amount = 1
		});

		// Assert
		var exception = await Assert.ThrowsAsync<RequiredDataException>(Act);

		exception.Message.Should().Be(string.Format(Messages.RequiredFieldFormat, Labels.Currency));
	}

	[Fact]
	public async Task Should_Exchange_Fail_NewCurrencyNotFound()
	{
		// Arrange
		var currencyBusiness = new CurrencyBusinessBuilder()
			.SetupMethodByShortName(null)
			.Build();

		var sut = new WalletBusiness(
			null,
			currencyBusiness,
			null);

		// Act
		Task Act() => sut.ExchangeAsync(new DtoExchangeTransaction
		{
			CurrentCurrency = "USD",
			NewCurrency = "ARS",
			Amount = 1
		});

		// Assert
		var exception = await Assert.ThrowsAsync<NotFoundException>(Act);

		exception.Message.Should().Be(string.Format(Messages.EntityNotFoundFormat, Labels.Currency));
	}

	[Fact]
	public async Task Should_Exchange_Fail_WalletNotFound()
	{
		// Arrange
		var currencyBusiness = new CurrencyBusinessBuilder()
			.SetupMethodByShortName(new Currency())
			.Build();

		var walletRepository = new WalletRepositoryBuilder()
			.SetupMethodGetWalletByUser(null)
			.Build();

		var sut = new WalletBusiness(
			null,
			currencyBusiness,
			walletRepository);

		// Act
		Task Act() => sut.ExchangeAsync(new DtoExchangeTransaction
		{
			CurrentCurrency = "USD",
			NewCurrency = "ARS",
			Amount = 1
		});

		// Assert
		var exception = await Assert.ThrowsAsync<NotFoundException>(Act);

		exception.Message.Should().Be(string.Format(Messages.EntityNotFoundFormat, Labels.Wallet));
	}

	[Fact]
	public async Task Should_Withdraw_Fail_AccountNotFound()
	{
		// Arrange
		var accountBusiness = new AccountBusinessBuilder()
			.SetupMethodGetAccountsByWallet(new List<Account>
			{
				new Account
				{
					IdAccount = 1,
					IdWallet = 1,
					IdCurrency = 1,
					Balance = 10,
					Status = 1
				},
				new Account
				{
					IdAccount = 2,
					IdWallet = 1,
					IdCurrency = 2,
					Balance = 100,
					Status = 1
				}
			})
			.SetupMethodAccountsByWalletAndCurrency(null)
			.Build();

		var currencyBusiness = new CurrencyBusinessBuilder()
			.SetupMethodByShortName(new Currency
			{
				IdCurrency = 1,
				ShortName = "ARS",
				Status = 1
			})
			.Build();

		var walletRepository = new WalletRepositoryBuilder()
			.SetupMethodGetWalletByUser(new Wallet())
			.Build();

		var sut = new WalletBusiness(
			accountBusiness,
			currencyBusiness,
			walletRepository);
		
		// Act
		Task Act() => sut.WithdrawAsync(new DtoExchangeTransaction()
		{
			CurrentCurrency = "USD",
			Amount = 12,
			NewCurrency = "ARS"
		});

		// Assert
		var exception = await Assert.ThrowsAsync<NotFoundException>(Act);

		exception.Message.Should().Be(string.Format(Messages.EntityNotFoundFormat, Labels.Account));
	}
	
	[Fact]
	public async Task Should_Withdraw_Fail_AccountIsInactive()
	{
		// Arrange
		var accountBusiness = new AccountBusinessBuilder()
			.SetupMethodGetAccountsByWallet(new List<Account>
			{
				new Account
				{
					IdAccount = 1,
					IdWallet = 1,
					IdCurrency = 1,
					Balance = 10,
					Status = 1
				},
				new Account
				{
					IdAccount = 2,
					IdWallet = 1,
					IdCurrency = 2,
					Balance = 100,
					Status = 1
				}
			})
			.SetupMethodAccountsByWalletAndCurrency(new Account
			{
				IdAccount = 1,
				IdWallet = 1,
				IdCurrency = 1,
				Balance = 0,
				Status = 0,
			})
			.Build();

		var currencyBusiness = new CurrencyBusinessBuilder()
			.SetupMethodByShortName(new Currency
			{
				IdCurrency = 1,
				ShortName = "ARS",
				Status = 1
			})
			.Build();

		var walletRepository = new WalletRepositoryBuilder()
			.SetupMethodGetWalletByUser(new Wallet())
			.Build();

		var sut = new WalletBusiness(
			accountBusiness,
			currencyBusiness,
			walletRepository);

		// Act
		Task Act() => sut.WithdrawAsync(new DtoExchangeTransaction()
		{
			CurrentCurrency = "USD",
			Amount = 12,
			NewCurrency = "ARS"
		});

		// Assert
		var exception = await Assert.ThrowsAsync<BadRequestException>(Act);

		exception.Message.Should().Be(Messages.AccountIsInactive);
	}

	[Fact]
	public async Task Should_Withdraw_Fail_InsufficientBalance()
	{
		// Arrange
		var accountBusiness = new AccountBusinessBuilder()
			.SetupMethodGetAccountsByWallet(new List<Account>
			{
				new Account
				{
					IdAccount = 1,
					IdWallet = 1,
					IdCurrency = 1,
					Balance = 10,
					Status = 1
				},
				new Account
				{
					IdAccount = 2,
					IdWallet = 1,
					IdCurrency = 2,
					Balance = 100,
					Status = 1
				}
			})
			.SetupMethodAccountsByWalletAndCurrency(new Account
			{
				IdAccount = 1,
				IdWallet = 1,
				IdCurrency = 1,
				Balance = 0,
				Status = 1
			})
			.Build();

		var currencyBusiness = new CurrencyBusinessBuilder()
			.SetupMethodByShortName(new Currency
			{
				IdCurrency = 1,
				ShortName = "ARS",
				Status = 1
			})
			.Build();

		var walletRepository = new WalletRepositoryBuilder()
			.SetupMethodGetWalletByUser(new Wallet())
			.Build();

		var sut = new WalletBusiness(
			accountBusiness,
			currencyBusiness,
			walletRepository);

		// Act
		Task Act() => sut.WithdrawAsync(new DtoExchangeTransaction()
		{
			CurrentCurrency = "USD",
			Amount = 12,
			NewCurrency = "ARS"
		});

		// Assert
		var exception = await Assert.ThrowsAsync<BadRequestException>(Act);

		exception.Message.Should().Be(Messages.InsufficientBalance);
	}
}