using KataDotNetPossumus.Repository.Sql.Interfaces;
using Moq;

namespace KataDotNetPossumus.BaseTest.Builders.Repository;

public class BaseRepositoryBuilder<T, TModel> where T : class, ISqlRepository<TModel> where TModel : class
{
	#region Mock Services

	protected readonly Mock<T> mock;

	#endregion

	#region Constructors

	public BaseRepositoryBuilder()
	{
		mock = new Mock<T>();

		SetupMethodSaveChanges();
	}

	#endregion

	#region Options
	
	protected BaseRepositoryBuilder<T, TModel> SetupMethodSaveChanges()
	{
		mock
			.Setup(p => p.SaveChangesAsync());

		return this;
	}

	#endregion

	#region Build

	public T Build()
	{
		return mock.Object;
	}

	#endregion
}