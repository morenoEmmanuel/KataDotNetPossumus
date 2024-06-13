using KataDotNetPossumus.Model.Entities;

namespace KataDotNetPossumus.Repository.Sql.Interfaces;

public interface IAccountRepository : ISqlRepository<Account>
{
	Task<List<Account>> FindByWalletAsync(int idWallet);
	Task<Account?> FindByWalletAndCurrencyAsync(int idWallet, int idCurrency);
}