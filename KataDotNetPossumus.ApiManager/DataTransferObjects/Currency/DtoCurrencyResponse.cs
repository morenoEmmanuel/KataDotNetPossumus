using System.Text.Json.Serialization;

namespace KataDotNetPossumus.ApiManager.DataTransferObjects.Currency;

public class DtoCurrencyResponse
{
	[JsonPropertyName("status")]
	public string Status { get; set; }

	[JsonPropertyName("updated_date")]
	public string UpdatedDate { get; set; }

	[JsonPropertyName("base_currency_code")]
	public string BaseCurrencyCode { get; set; }

	[JsonPropertyName("amount")]
	public int Amount { get; set; }

	[JsonPropertyName("base_currency_name")]
	public string BaseCurrencyName { get; set; }

	[JsonPropertyName("rates")]
	public Rates Rates { get; set; }
}

public class GBP
{
	[JsonPropertyName("currency_name")]
	public string CurrencyName { get; set; }

	[JsonPropertyName("rate")]
	public string Rate { get; set; }

	[JsonPropertyName("rate_for_amount")]
	public string RateForAmount { get; set; }
}

public class Rates
{
	[JsonPropertyName("GBP")]
	public GBP GBP { get; set; }
}
