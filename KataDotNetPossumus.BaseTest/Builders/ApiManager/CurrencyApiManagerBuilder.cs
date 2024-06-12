using KataDotNetPossumus.ApiManager.Interfaces;
using Moq;

namespace KataDotNetPossumus.BaseTest.Builders.ApiManager;

public class CurrencyApiManagerBuilder
{
	#region Mock Services

	private readonly Mock<ICurrencyApiManager> mock;

	#endregion

	#region Constructors

	public CurrencyApiManagerBuilder()
	{
		mock = new Mock<ICurrencyApiManager>();
	}

	#endregion

	#region Option

	public CurrencyApiManagerBuilder SetupMethodGetNewAmount(double response)
	{
		mock
			.Setup(p => p.GetNewAmountAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<double>()))
			.ReturnsAsync(response);

		return this;
	}

	#endregion

	#region Build

	public ICurrencyApiManager Build()
	{
		return mock.Object;
	}

	#endregion
}