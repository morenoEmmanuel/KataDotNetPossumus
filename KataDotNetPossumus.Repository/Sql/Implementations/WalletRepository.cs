﻿using KataDotNetPossumus.CurrentContext;
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
	/// <returns>The wallet.</returns>
	public async Task<Wallet?> GetWalletByUserAsync()
	{
		return await FindAsync(p => p.IdUser == ContextData.UserId
									&& p.Status == (int)Enumerations.Enumerations.EntityStatus.ACTIVE);
	}

	#endregion
}