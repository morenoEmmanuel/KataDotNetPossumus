namespace KataDotNetPossumus.ApiManager.Interfaces;

public interface ICurrencyApiManager
{
	Task<double> GetNewAmountAsync(string from, string to, double amount);
}