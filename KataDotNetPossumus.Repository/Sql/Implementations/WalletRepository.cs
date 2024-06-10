using KataDotNetPossumus.CurrentContext;
using KataDotNetPossumus.Model.Entities;
using KataDotNetPossumus.Model.SqlContext;
using KataDotNetPossumus.Repository.Sql.Interfaces;

namespace KataDotNetPossumus.Repository.Sql.Implementations;

public class WalletRepository : BaseSqlRepository<Wallet>, IWalletRepository
{
	#region Constructors

	public WalletRepository(
		ContextData contextData,
		KataDotNetPossumusDbContext dbContext)
		: base(contextData, dbContext)
	{
	}

	#endregion
}