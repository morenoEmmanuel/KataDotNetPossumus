using KataDotNetPossumus.Model.Entities;

namespace KataDotNetPossumus.Repository.Sql.Interfaces;

public interface ICurrencyRepository : ISqlRepository<Currency>
{
	Task<Currency?> ByShortName(string shortName);
}