using KataDotNetPossumus.Business.Interfaces;
using KataDotNetPossumus.Model.Entities;
using KataDotNetPossumus.Repository.Sql.Interfaces;

namespace KataDotNetPossumus.Business.Implementations;

public class AccountHistoryBusiness : IAccountHistoryBusiness
{

	#region Dependencies
	
	private readonly IAccountHistoryRepository accountHistoryRepository;

	#endregion

	#region Constructors

	public AccountHistoryBusiness(IAccountHistoryRepository accountHistoryRepository)
	{
		this.accountHistoryRepository = accountHistoryRepository;
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// Saves an accountHistory.
	/// </summary>
	/// <param name="entity">
	///		<para>The request data.</para>
	/// </param>
	/// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
	public async Task SaveAccountHistoryAsync(AccountHistory entity)
	{
		await accountHistoryRepository.CreateAsync(entity);

		await accountHistoryRepository.SaveChangesAsync(); 
	}

	#endregion
}