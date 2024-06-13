using KataDotNetPossumus.Business.Interfaces;
using KataDotNetPossumus.Exceptions;
using KataDotNetPossumus.Model.DataTransferObject.Wallet;
using KataDotNetPossumus.Repository.Sql.Interfaces;
using KataDotNetPossumus.Resources;
using static KataDotNetPossumus.Enumerations.Enumerations;

namespace KataDotNetPossumus.Business.Implementations;

public class WalletBusiness : IWalletBusiness
{
	#region Dependencies
	
	private readonly IAccountBusiness accountBusiness;
	private readonly ICurrencyBusiness currencyBusiness;
	private readonly IWalletRepository walletRepository;

	#endregion

	#region Constructors

	public WalletBusiness(
		IAccountBusiness accountBusiness,
		ICurrencyBusiness currencyBusiness,
		IWalletRepository walletRepository)
	{
		this.accountBusiness = accountBusiness;
		this.currencyBusiness = currencyBusiness;
		this.walletRepository = walletRepository;
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// Gets the balance information.
	/// </summary>
	/// <returns>The balance information.</returns>
	public async Task<DtoBalance> GetBalanceByUserAsync()
	{
		var wallet = await walletRepository.GetWalletByUserAsync();

		if (wallet == null) throw new NotFoundException(Labels.Wallet);

		var accounts =  await accountBusiness.GetAccountsByWalletAsync(wallet.IdWallet);
		
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

	/// <summary>
	/// Deposits an amount into a wallet.
	/// </summary>
	/// <param name="request">
	///		<para>The request data.</para>
	/// </param>
	/// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
	public async Task DepositAsync(DtoTransactionRequest? request)
	{
		var deposit = await ValidateRequestAsync(request, TransactionType.DEPOSIT);
		
		await accountBusiness.SaveAccountAsync(deposit);
	}

	/// <summary>
	/// Exchanges currencies of a wallet.
	/// </summary>
	/// <param name="request">
	///		<para>The request data.</para>
	/// </param>
	/// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
	public async Task ExchangeAsync(DtoExchangeTransaction? request)
	{
		var transaction = await ValidateRequestAsync(request, TransactionType.EXCHANGE, request?.NewCurrency);

		await accountBusiness.ExchangeAsync(transaction, request);
	}

	/// <summary>
	/// Withdraws an amount from a wallet.
	/// </summary>
	/// <param name="request">
	///		<para>The request data.</para>
	/// </param>
	/// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
	public async Task WithdrawAsync(DtoTransactionRequest? request)
	{
		var withdraw = await ValidateRequestAsync(request, TransactionType.WITHDRAW);

		await accountBusiness.SaveAccountAsync(withdraw, false);
	}

	#endregion

	#region Private Methods

	private async Task<DtoTransaction> ValidateRequestAsync(DtoTransactionRequest? request, TransactionType transactionType, string? newCurrency = null)
	{
		if (request == null) throw new BadRequestException(nameof(request));
		
		if (request.Amount is null or <= 0) throw new RequiredDataException(Labels.Amount);
		
		var idNewCurrency = default(int);
		
		if (transactionType == TransactionType.EXCHANGE)
		{
			if (string.IsNullOrWhiteSpace(newCurrency)) throw new RequiredDataException(Labels.Currency);
			
			var exchangeCurrency = await currencyBusiness.GetByShortNameAsync(newCurrency);

			if (exchangeCurrency == null) throw new NotFoundException(Labels.Currency);

			idNewCurrency = exchangeCurrency.IdCurrency;
		}
		
		var wallet = await walletRepository.GetWalletByUserAsync();

		if (wallet == null) throw new NotFoundException(Labels.Wallet);

		if (string.IsNullOrWhiteSpace(request.CurrentCurrency)) throw new NotFoundException(Labels.Currency);

		var currency = await currencyBusiness.GetByShortNameAsync(request.CurrentCurrency);

		if (currency == null) throw new NotFoundException(Labels.Currency);

		var account = await accountBusiness.GetAccountsByWalletAndCurrencyAsync(wallet.IdWallet, currency.IdCurrency);
		
		if (account == null && transactionType != TransactionType.DEPOSIT) throw new NotFoundException(Labels.Account);

		if (account != null && account.Status == (int)EntityStatus.DELETED)
		{
			throw new BadRequestException(Messages.AccountIsInactive);
		}

		if (transactionType != TransactionType.DEPOSIT && (account.Balance < request.Amount))
		{
			throw new BadRequestException(Messages.InsufficientBalance);
		}

		return new DtoTransaction
		{
			IdAccount = account?.IdAccount ?? default(int),
			IdWallet = wallet.IdWallet,
			IdCurrency = currency.IdCurrency,
			TransactionAmount = request.Amount.Value,
			IdNewCurrency = idNewCurrency
		};
	}

	#endregion
}