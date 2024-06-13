using KataDotNetPossumus.ApiManager.Interfaces;
using KataDotNetPossumus.SettingHelper;
using Skelp.ApiPlant.CurrencyApi.Client;

namespace KataDotNetPossumus.ApiManager.Implementations;

public class CurrencyApiManager : ICurrencyApiManager
{
	#region Dependencies

	private readonly AppSettingHelper appSettingHelper;

	#endregion

	#region Constructors

	public CurrencyApiManager(AppSettingHelper appSettingHelper)
	{
		this.appSettingHelper = appSettingHelper;
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// Gets the amount resulting from the exchange of one currency for another.
	/// </summary>
	/// <param name="from">
	///		<para>The currency to change.</para>
	/// </param>
	/// <param name="to">
	///		<para>The currency to obtain.</para>
	/// </param>
	/// <param name="amount">
	///		<para>The amount to change.</para>
	/// </param>
	/// <returns>The the amount resulting from the exchange of one currency for another.</returns>
	public async Task<double> GetNewAmountAsync(string from, string to, double amount)
	{
		var client = new CurrencyApiClient(appSettingHelper.CurrencyApiKey);

		var conversion = await client.ConvertAsync(from: from, to: to, amount: Convert.ToDecimal(amount));

		return decimal.ToDouble(conversion.Rates.First().Value.RateForAmount);
	}

	#endregion
}