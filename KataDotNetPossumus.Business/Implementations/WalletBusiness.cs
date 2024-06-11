using KataDotNetPossumus.Business.Interfaces;
using KataDotNetPossumus.Exceptions;
using KataDotNetPossumus.Model.DataTransferObject.Wallet;
using KataDotNetPossumus.Repository.Sql.Interfaces;
using KataDotNetPossumus.Resources;

namespace KataDotNetPossumus.Business.Implementations;

public class WalletBusiness : IWalletBusiness
{
	#region Dependencies
	
	private readonly IAccountRepository accountRepository;
	private readonly IWalletRepository walletRepository;

	#endregion

	#region Constructors

	public WalletBusiness(
		IAccountRepository accountRepository,
		IWalletRepository walletRepository)
	{
		this.accountRepository = accountRepository;
		this.walletRepository = walletRepository;
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// Gets the balance information by user ID.
	/// </summary>
	/// <param name="idUser">
	///		<para>The user ID.</para>
	/// </param>
	/// <returns>The balance information.</returns>
	/// <exception cref="NotFoundException">If the user is not found.</exception>
	public async Task<DtoBalance> GetBalanceByUserAsync(int? idUser)
	{
		if (!idUser.HasValue) throw new NotFoundException(Labels.User);
		
		var wallet = await walletRepository.GetWalletByUser(idUser.Value);

		if (wallet == null) throw new NotFoundException(Labels.Wallet);

		var accounts =  await accountRepository.GetAccountsByWalletAsync(wallet.IdWallet);
		
		if (accounts == null) throw new NotFoundException(Labels.Account);

		var response = new DtoBalance
		{
			AccountsBalances = new List<DtoAccountBalance>()
		};

		foreach (var account in accounts)
		{
			response.AccountsBalances.Add(new DtoAccountBalance
			{
				Balance = account.Balance.ToString("n2"),
				Currency = account.Currency?.ShortName ?? string.Empty
			});
		}

		return response;
	}

	#endregion
}