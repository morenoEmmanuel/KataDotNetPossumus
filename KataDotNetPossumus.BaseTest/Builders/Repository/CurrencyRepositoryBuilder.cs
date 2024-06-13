using KataDotNetPossumus.Model.Entities;
using KataDotNetPossumus.Repository.Sql.Interfaces;
using Moq;

namespace KataDotNetPossumus.BaseTest.Builders.Repository;

public class CurrencyRepositoryBuilder : BaseRepositoryBuilder<ICurrencyRepository, Currency>
{
	#region Options

	public CurrencyRepositoryBuilder SetupMethodByShortName(Currency response)
	{
		mock
			.Setup(p => p.ByShortName(It.IsAny<string>()))
			.ReturnsAsync(response);

		return this;
	}

	#endregion
}