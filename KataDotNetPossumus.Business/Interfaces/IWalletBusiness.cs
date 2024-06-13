using KataDotNetPossumus.Model.DataTransferObject.Wallet;

namespace KataDotNetPossumus.Business.Interfaces;

public interface IWalletBusiness
{
	Task<DtoBalance> GetBalanceByUserAsync();
	Task DepositAsync(DtoTransactionRequest? request);
	Task ExchangeAsync(DtoExchangeTransaction? request);
	Task WithdrawAsync(DtoTransactionRequest? request);
}