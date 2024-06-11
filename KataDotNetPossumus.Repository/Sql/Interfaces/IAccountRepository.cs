using KataDotNetPossumus.Model.Entities;

namespace KataDotNetPossumus.Repository.Sql.Interfaces;

public interface IAccountRepository : ISqlRepository<Account>
{
	Task<List<Account>> GetAccountsByWalletAsync(int idWallet);
}