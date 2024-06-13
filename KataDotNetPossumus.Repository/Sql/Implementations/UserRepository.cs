using KataDotNetPossumus.CurrentContext;
using KataDotNetPossumus.Model.Entities;
using KataDotNetPossumus.Model.SqlContext;
using KataDotNetPossumus.Repository.Sql.Interfaces;
using static KataDotNetPossumus.Enumerations.Enumerations;

namespace KataDotNetPossumus.Repository.Sql.Implementations;

public class UserRepository : BaseSqlRepository<User>, IUserRepository
{
	#region Constructors

	public UserRepository(
		ContextData contextData,
		KataDotNetPossumusDbContext dbContext)
		: base(contextData, dbContext)
	{
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// 
	/// Indicates if a user is valid.
	/// </summary>
	/// <param name="username">
	///		<para>The username.</para>
	/// </param>
	/// <param name="password">
	///		<para>The password.</para>
	/// </param>
	/// <returns>True if the user is valid, false other case.</returns>
	public async Task<User?> ValidateUserAsync(string username, string password)
	{
		return await FindAsync(p =>
			p.Name == username
			&& p.Password == password
			&& p.Status == (int)EntityStatus.ACTIVE);
	}

	#endregion

}