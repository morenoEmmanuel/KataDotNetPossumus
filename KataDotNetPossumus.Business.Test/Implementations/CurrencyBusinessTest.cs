using FluentAssertions;
using KataDotNetPossumus.BaseTest.Builders.Repository;
using KataDotNetPossumus.Business.Implementations;
using KataDotNetPossumus.Model.Entities;
using Xunit;

namespace KataDotNetPossumus.BaseTest.Builders.Business;

public class CurrencyBusinessTest
{
	#region Test

	[Fact]
	public async Task Should_GetByShortName_Succeed()
	{
		// Arrange
		var currencyRepository = new CurrencyRepositoryBuilder()
			.SetupMethodByShortName(new Currency
			{
				IdCurrency = 1,
				ShortName = "USD",
				Status = 1
			})
			.Build();

		var sut = new CurrencyBusiness(currencyRepository);

		// Act
		var act = await sut.GetByShortNameAsync("USD");
		
		// Assert
		act.ShortName.Should().Be("USD");
	}

	#endregion
}