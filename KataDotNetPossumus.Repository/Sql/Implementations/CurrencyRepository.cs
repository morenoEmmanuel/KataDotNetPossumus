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

	#region Public Methods

	/// <summary>
	/// Gets the currency by short name.
	/// </summary>
	/// <param name="shortName">
	///		<para>The currency short name.</para>
	/// </param>
	/// <returns>The currency.</returns>
	public async Task<Currency?> ByShortName(string shortName)
	{
		return await FindNoTrackingAsync(p => p.ShortName == shortName
		                                      && p.Status == (int)Enumerations.Enumerations.EntityStatus.ACTIVE);
	}

	#endregion
}