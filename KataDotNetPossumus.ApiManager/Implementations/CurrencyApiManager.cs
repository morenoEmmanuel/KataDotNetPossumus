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

	public async Task<double> GetNewAmountAsync(string from, string to, double amount)
	{
		var client = new CurrencyApiClient(appSettingHelper.CurrencyApiKey);

		var conversion = await client.ConvertAsync(from: from, to: to, amount: Convert.ToDecimal(amount));

		return decimal.ToDouble(conversion.Amount);
	}

	#endregion
}