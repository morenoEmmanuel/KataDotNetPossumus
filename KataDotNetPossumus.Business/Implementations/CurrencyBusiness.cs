using KataDotNetPossumus.Business.Interfaces;
using KataDotNetPossumus.Model.Entities;
using KataDotNetPossumus.Repository.Sql.Interfaces;

namespace KataDotNetPossumus.Business.Implementations;

public class CurrencyBusiness : ICurrencyBusiness
{
	#region Dependencies
	
	private readonly ICurrencyRepository currencyRepository;

	#endregion

	#region Constructors

	public CurrencyBusiness(ICurrencyRepository currencyRepository)
	{
		this.currencyRepository = currencyRepository;
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// Gets a currency by short name.
	/// </summary>
	/// <param name="shortName">
	///		<para>The short name.</para>
	/// </param>
	/// <returns>The currency.</returns>
	public async Task<Currency?> GetByShortNameAsync(string shortName)
	{
		return await currencyRepository.ByShortName(shortName);
	}

	#endregion
}