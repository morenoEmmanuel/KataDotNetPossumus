using KataDotNetPossumus.Model.Entities;
using KataDotNetPossumus.Repository.Sql.Interfaces;
using Moq;

namespace KataDotNetPossumus.BaseTest.Builders.Repository;

public class UserRepositoryBuilder : BaseRepositoryBuilder<IUserRepository, User>
{
	#region Options

	public UserRepositoryBuilder SetupMethodValidateUser(User? response)
	{
		mock
			.Setup(p => p.ValidateUserAsync(It.IsAny<string>(), It.IsAny<string>()))
			.ReturnsAsync(response);

		return this;
	}

	#endregion
}