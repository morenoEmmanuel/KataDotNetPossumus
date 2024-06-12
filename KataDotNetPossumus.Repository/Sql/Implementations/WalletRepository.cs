using KataDotNetPossumus.CurrentContext;
using KataDotNetPossumus.Model.Entities;
using KataDotNetPossumus.Model.SqlContext;
using KataDotNetPossumus.Repository.Sql.Interfaces;

namespace KataDotNetPossumus.Repository.Sql.Implementations;

public class WalletRepository : BaseSqlRepository<Wallet>, IWalletRepository
{
	#region Constructors

	public WalletRepository(
		ContextData contextData,
		KataDotNetPossumusDbContext dbContext)
		: base(contextData, dbContext)
	{
	}

	#endregion

	#region Public Methods
	
	/// <summary>
	/// Gets the wallet by user ID.
	/// </summary>
	/// <param name="idUser">
	///		<para>The user ID.</para>
	/// </param>
	/// <returns>The wallet.</returns>
	public async Task<Wallet?> GetWalletByUserAsync(int idUser)
	{
		return await FindAsync(p => p.IdUser == idUser
		                            && p.Status == (int)Enumerations.Enumerations.EntityStatus.ACTIVE);
	}

	#endregion
}