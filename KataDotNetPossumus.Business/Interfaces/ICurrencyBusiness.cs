using KataDotNetPossumus.Model.Entities;

namespace KataDotNetPossumus.Business.Interfaces;

public interface ICurrencyBusiness
{
	Task<Currency?> GetByShortNameAsync(string shortName);
}