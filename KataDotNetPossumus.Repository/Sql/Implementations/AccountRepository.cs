using KataDotNetPossumus.CurrentContext;
using KataDotNetPossumus.Model.Entities;
using KataDotNetPossumus.Model.SqlContext;
using KataDotNetPossumus.Repository.Sql.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KataDotNetPossumus.Repository.Sql.Implementations;

public class AccountRepository : BaseSqlRepository<Account>, IAccountRepository
{
	#region Constructors

	public AccountRepository(
		ContextData contextData,
		KataDotNetPossumusDbContext dbContext)
		: base(contextData, dbContext)
	{
	}

	#endregion


	#region Public Methods

	/// <summary>
	/// Gets the accounts by wallet ID.
	/// </summary>
	/// <param name="idWallet">
	///		<para>The wallet ID.</para>
	/// </param>
	/// <returns>The accounts.</returns>
	public async Task<List<Account>> FindByWalletAsync(int idWallet)
	{
		return await QueryableNoTracking(p =>
			p.IdWallet == idWallet 
			&& p.Status == (int)Enumerations.Enumerations.EntityStatus.ACTIVE)
			.Include(p => p.Currency)
			.ToListAsync();
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
	public async Task<Account?> FindByWalletAndCurrencyAsync(int idWallet, int idCurrency)
	{
		return await FindNoTrackingAsync(p => p.IdWallet == idWallet
											  && p.IdCurrency == idCurrency
											  && p.Status == (int)Enumerations.Enumerations.EntityStatus.ACTIVE);
	}

	#endregion
}