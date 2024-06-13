using KataDotNetPossumus.Model.Entities;
using KataDotNetPossumus.Repository.Sql.Interfaces;
using Moq;

namespace KataDotNetPossumus.BaseTest.Builders.Repository;

public class WalletRepositoryBuilder : BaseRepositoryBuilder<IWalletRepository, Wallet>
{
	#region Options

	public WalletRepositoryBuilder SetupMethodGetWalletByUser(Wallet? response)
	{
		mock
			.Setup(p => p.GetWalletByUserAsync())
			.ReturnsAsync(response);

		return this;
	}

	#endregion
}