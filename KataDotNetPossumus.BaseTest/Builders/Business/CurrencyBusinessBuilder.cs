using KataDotNetPossumus.Business.Interfaces;
using KataDotNetPossumus.Model.Entities;
using Moq;

namespace KataDotNetPossumus.BaseTest.Builders.Business;

public class CurrencyBusinessBuilder
{
	#region Mock Services

	private readonly Mock<ICurrencyBusiness> mock;

	#endregion

	#region Constructors

	public CurrencyBusinessBuilder()
	{
		mock = new Mock<ICurrencyBusiness>();
	}

	#endregion

	#region Options

	public CurrencyBusinessBuilder SetupMethodSaveAccountHistoryAsync(Currency response)
	{
		mock
			.Setup(p => p.GetByShortNameAsync(It.IsAny<string>()))
			.ReturnsAsync(response);

		return this;
	}

	#endregion

	#region Build

	public ICurrencyBusiness Build()
	{
		return mock.Object;
	}

	#endregion
}