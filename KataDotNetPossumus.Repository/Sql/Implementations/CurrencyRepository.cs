using KataDotNetPossumus.CurrentContext;
using KataDotNetPossumus.Model.Entities;
using KataDotNetPossumus.Model.SqlContext;
using KataDotNetPossumus.Repository.Sql.Interfaces;

namespace KataDotNetPossumus.Repository.Sql.Implementations;

public class CurrencyRepository : BaseSqlRepository<Currency>, ICurrencyRepository
{
	#region Constructors

	public CurrencyRepository(
		ContextData contextData,
		KataDotNetPossumusDbContext dbContext)
		: base(contextData, dbContext)
	{
	}

	#endregion
}