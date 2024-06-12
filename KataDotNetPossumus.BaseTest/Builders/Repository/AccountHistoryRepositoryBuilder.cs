using KataDotNetPossumus.Model.Entities;
using KataDotNetPossumus.Repository.Sql.Interfaces;
using Moq;

namespace KataDotNetPossumus.BaseTest.Builders.Repository;

public class AccountHistoryRepositoryBuilder : BaseRepositoryBuilder<IAccountHistoryRepository, AccountHistory>
{
	#region Options

	public AccountHistoryRepositoryBuilder SetupMethodCreate()
	{
		mock
			.Setup(p => p.CreateAsync(It.IsAny<AccountHistory>()))
			.Verifiable();

		return this;
	}

	#endregion
}