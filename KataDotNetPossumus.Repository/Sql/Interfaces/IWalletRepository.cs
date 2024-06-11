using KataDotNetPossumus.Model.Entities;

namespace KataDotNetPossumus.Repository.Sql.Interfaces;

public interface IWalletRepository : ISqlRepository<Wallet>
{
	Task<Wallet?> GetWalletByUser(int idUser);
}