using Azure.Core;
using KataDotNetPossumus.ApiManager.Interfaces;
using KataDotNetPossumus.Business.Interfaces;
using KataDotNetPossumus.CurrentContext;
using KataDotNetPossumus.Model.DataTransferObject.Wallet;
using KataDotNetPossumus.Model.Entities;
using KataDotNetPossumus.Repository.Sql.Interfaces;

namespace KataDotNetPossumus.Business.Implementations;

public class AccountBusiness : IAccountBusiness
{
	#region Dependencies

	private readonly IAccountRepository accountRepository;
	private readonly IAccountHistoryBusiness accountHistoryBusiness;
	private readonly ContextData contextData;
	private readonly ICurrencyBusiness currencyBusiness;
	private readonly ICurrencyApiManager currencyApiManager;

	#endregion

	#region Constructors

	public AccountBusiness(
		IAccountRepository accountRepository,
		IAccountHistoryBusiness accountHistoryBusiness,
		ContextData contextData,
		ICurrencyApiManager currencyApiManager,
		ICurrencyBusiness currencyBusiness)
	{
		this.accountRepository = accountRepository;
		this.accountHistoryBusiness = accountHistoryBusiness;
		this.contextData = contextData;
		this.currencyApiManager = currencyApiManager;
		this.currencyBusiness = currencyBusiness;
	}

	#endregion

	#region Public Methods

	/// <summary>
	///  Gets the accounts by wallet ID.
	/// </summary>
	/// <param name="idWallet">
	///		<para>The wallet ID.</para>
	/// </param>
	/// <returns>The accounts.</returns>
	public async Task<List<Account>> GetAccountsByWalletAsync(int idWallet)
	{
		return await accountRepository.FindByWalletAsync(idWallet);
	}

	/// <summary>
	///  Gets the account by wallet ID and currency ID.
	/// </summary>
	/// <param name="idWallet">
	///		<para>The wallet ID.</para>
	/// </param>
	/// <param name="idCurrency">
	///		<para>The currency ID.</para>
	/// </param>
	/// <returns>The account.</returns>
	public async Task<Account?> GetAccountsByWalletAndCurrencyAsync(int idWallet, int idCurrency)
	{
		return await accountRepository.FindByWalletAndCurrencyAsync(idWallet, idCurrency);
	}

	public async Task ExchangeAsync(DtoTransaction requestData, DtoExchangeTransaction request)
	{
		//primero obtener cuanta plata se debe depositar
		var newCurrency = await currencyBusiness.GetByShortNameAsync(request.NewCurrency);

		var newAmount = await currencyApiManager.GetNewAmountAsync(request.CurrentCurrency, request.NewCurrency, request.Amount.Value);

		//deposit
		await SaveAccountAsync(new DtoTransaction
		{
			IdWallet = requestData.IdWallet,
			IdAccount = requestData.IdAccount,
			IdCurrency = newCurrency.IdCurrency,
			TransactionAmount = newAmount
		});

		//withdraw
		await SaveAccountAsync(requestData, false);
	}

	/// <summary>
	/// Saves a deposit.
	/// </summary>
	/// <param name="requestData">
	///		<para>The request data.</para>
	/// </param>
	/// <param name="isDeposit">
	///		<para>Indicates if the transaction is a deposit.</para>
	/// </param>
	/// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
	public async Task SaveAccountAsync(DtoTransaction requestData, bool isDeposit = true)
	{
		Account? account;

		var accountHistory = new AccountHistory
		{
			EditedBy = contextData.UserId ?? default,
			EditionDate = DateTime.UtcNow
		};

		if (requestData.IdAccount > 0 || !isDeposit)
		{
			account = await accountRepository.FindAsync(requestData.IdAccount);

			accountHistory.OldBalance = account.Balance;
		}
		else
		{
			account = new Account
			{
				IdWallet = requestData.IdWallet,
				IdCurrency = requestData.IdCurrency,
				Status = (int)Enumerations.Enumerations.EntityStatus.ACTIVE
			};

			await accountRepository.CreateAsync(account);

			accountHistory.OldBalance = default;
		}
		
		if (isDeposit)
		{
			account.Balance += requestData.TransactionAmount;
		}
		else
		{
			account.Balance -= requestData.TransactionAmount;
		}

		await accountRepository.SaveChangesAsync();

		accountHistory.IdAccount = account.IdAccount;

		await accountHistoryBusiness.SaveAccountHistoryAsync(accountHistory);
	}

	#endregion
}