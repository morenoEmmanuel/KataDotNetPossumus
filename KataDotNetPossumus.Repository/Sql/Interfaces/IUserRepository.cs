using KataDotNetPossumus.Model.Entities;

namespace KataDotNetPossumus.Repository.Sql.Interfaces;

public interface IUserRepository : ISqlRepository<User>
{
	Task<User?> GetByUserNameAndPasswordAsync(string username, string password);
}