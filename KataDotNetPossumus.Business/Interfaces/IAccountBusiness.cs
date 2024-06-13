using KataDotNetPossumus.Model.DataTransferObject.Wallet;
using KataDotNetPossumus.Model.Entities;

namespace KataDotNetPossumus.Business.Interfaces;

public interface IAccountBusiness
{
	Task<List<Account>> GetAccountsByWalletAsync(int idWallet);
	Task<Account?> GetAccountsByWalletAndCurrencyAsync(int idWallet, int idCurrency);
	Task ExchangeAsync(DtoTransaction requestData, DtoExchangeTransaction exchangeData);
	Task SaveAccountAsync(DtoTransaction requestData, bool isDeposit = true);
}