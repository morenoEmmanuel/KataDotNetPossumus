using FluentAssertions;
using KataDotNetPossumus.BaseTest.Builders.Repository;
using KataDotNetPossumus.Business.Implementations;
using KataDotNetPossumus.Exceptions;
using KataDotNetPossumus.Model.Entities;
using KataDotNetPossumus.Resources;
using Xunit;

namespace KataDotNetPossumus.BaseTest.Builders.Business;

public class WalletBusinessTest
{
	[Fact]
	public async Task Should_GetBalanceByUser_Fail_IdUserRequired()
	{
		// Arrange
		var sut = new WalletBusiness(
			null,
			null,
			null);

		// Act
		Task Act() => sut.GetBalanceByUserAsync();

		// Assert
		var exception = await Assert.ThrowsAsync<RequiredDataException>(Act);

		exception.Message.Should().Be(string.Format(Messages.RequiredFieldFormat, Labels.User));
	}

	[Fact]
	public async Task Should_GetBalanceByUser_Fail_WalletNotFound()
	{
		// Arrange
		var walletRepository = new WalletRepositoryBuilder()
			.SetupMethodGetWalletByUser(null)
			.Build();

		var sut = new WalletBusiness(
			null,
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
		var accountRepository = new AccountRepositoryBuilder()
			.SetupMethodGetAccountsByWallet(null)
			.Build();

		var walletRepository = new WalletRepositoryBuilder()
			.SetupMethodGetWalletByUser(new Wallet())
			.Build();

		var sut = new WalletBusiness(
			null,
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
		var accountRepository = new AccountRepositoryBuilder()
			.SetupMethodGetAccountsByWallet(new List<Account>
			{
				new Account
				{
					IdAccount = 1,
					IdWallet = 1,
					Currency = new Currency
					{
						IdCurrency = 1,
						ShortName = "USD",
						Status = 1
					},
					Balance = 100,
					Status = 1
				},
				new Account
				{
					IdAccount = 2,
					IdWallet = 1,
					Currency = new Currency
					{
						IdCurrency = 2,
						ShortName = "ARG",
						Status = 1
					},
					Balance = 1000,
					Status = 1
				}
			})
			.Build();

		var walletRepository = new WalletRepositoryBuilder()
			.SetupMethodGetWalletByUser(new Wallet())
			.Build();

		var sut = new WalletBusiness(
			null,
			null,
			walletRepository);

		// Act
		var act = await sut.GetBalanceByUserAsync();

		// Assert
		act?.AccountsBalances.Should().HaveCount(2);
	}
}