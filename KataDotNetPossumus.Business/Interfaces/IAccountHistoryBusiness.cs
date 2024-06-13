using KataDotNetPossumus.Model.Entities;

namespace KataDotNetPossumus.Business.Interfaces;

public interface IAccountHistoryBusiness
{
	Task SaveAccountHistoryAsync(AccountHistory entity);
}