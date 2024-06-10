using KataDotNetPossumus.CurrentContext;
using KataDotNetPossumus.Model.Entities;
using KataDotNetPossumus.Model.SqlContext;
using KataDotNetPossumus.Repository.Sql.Interfaces;

namespace KataDotNetPossumus.Repository.Sql.Implementations;

public class AccountRepository : BaseSqlRepository<Account>, IAccountRepository
{
	#region Constructors

	public AccountRepository(
		ContextData contextData,
		KataDotNetPossumusDbContext dbContext)
		: base(contextData, dbContext)
	{
	}

	#endregion
}