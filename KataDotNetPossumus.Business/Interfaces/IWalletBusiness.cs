using KataDotNetPossumus.Model.DataTransferObject.Wallet;

namespace KataDotNetPossumus.Business.Interfaces;

public interface IWalletBusiness
{
	Task<DtoBalance> GetBalanceByUserAsync(int? idUser);
}