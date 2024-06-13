using KataDotNetPossumus.Business.Interfaces;
using KataDotNetPossumus.Model.Entities;
using Moq;

namespace KataDotNetPossumus.BaseTest.Builders.Business;

public class AccountBusinessBuilder
{
	#region Mock Services

	private readonly Mock<IAccountBusiness> mock;

	#endregion

	#region Constructors

	public AccountBusinessBuilder()
	{
		mock = new Mock<IAccountBusiness>();
	}

	#endregion

	#region Options

	public AccountBusinessBuilder SetupMethodGetAccountsByWallet(List<Account> response)
	{
		mock
			.Setup(p => p.GetAccountsByWalletAsync(It.IsAny<int>()))
			.ReturnsAsync(response);

		return this;
	}

	public AccountBusinessBuilder SetupMethodAccountsByWalletAndCurrency(Account response)
	{
		mock
			.Setup(p => p.GetAccountsByWalletAndCurrencyAsync(It.IsAny<int>(), It.IsAny<int>()))
			.ReturnsAsync(response);

		return this;
	}

	#endregion

	#region Build

	public IAccountBusiness Build()
	{
		return mock.Object;
	}

	#endregion
}