using KataDotNetPossumus.Model.Entities;
using KataDotNetPossumus.Repository.Sql.Interfaces;
using Moq;

namespace KataDotNetPossumus.BaseTest.Builders.Repository;

public class AccountRepositoryBuilder : BaseRepositoryBuilder<IAccountRepository, Account>
{
	#region Options

	public AccountRepositoryBuilder SetupMethodGetAccountsByWallet(List<Account> response)
	{
		mock
			.Setup(p => p.FindByWalletAsync(It.IsAny<int>()))
			.ReturnsAsync(response);

		return this;
	}

	#endregion
}