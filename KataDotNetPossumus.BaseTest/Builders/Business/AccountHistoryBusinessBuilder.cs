using KataDotNetPossumus.Business.Interfaces;
using KataDotNetPossumus.Model.Entities;
using Moq;

namespace KataDotNetPossumus.BaseTest.Builders.Business;

public class AccountHistoryBusinessBuilder
{
	#region Mock Services

	private readonly Mock<IAccountHistoryBusiness> mock;

	#endregion

	#region Constructors

	public AccountHistoryBusinessBuilder()
	{
		mock = new Mock<IAccountHistoryBusiness>();
	}

	#endregion

	#region Options

	public AccountHistoryBusinessBuilder SetupMethodSaveAccountHistory()
	{
		mock
			.Setup(p => p.SaveAccountHistoryAsync(It.IsAny<AccountHistory>()))
			.Verifiable();

		return this;
	}

	#endregion

	#region Build

	public IAccountHistoryBusiness Build()
	{
		return mock.Object;
	}

	#endregion
}