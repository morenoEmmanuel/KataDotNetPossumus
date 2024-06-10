using KataDotNetPossumus.CurrentContext;
using KataDotNetPossumus.Model.Entities;
using KataDotNetPossumus.Model.SqlContext;
using KataDotNetPossumus.Repository.Sql.Interfaces;

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
}