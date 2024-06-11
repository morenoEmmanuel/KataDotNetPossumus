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
	public async Task<List<Account>> GetAccountsByWalletAsync(int idWallet)
	{
		return await QueryableNoTracking(p =>
			p.IdWallet == idWallet 
			&& p.Status == (int)Enumerations.Enumerations.EntityStatus.ACTIVE)
			.Include(p => p.Currency)
			.ToListAsync();
	}

	#endregion
}