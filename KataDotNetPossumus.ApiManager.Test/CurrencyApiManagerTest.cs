using FluentAssertions;
using KataDotNetPossumus.ApiManager.Implementations;
using KataDotNetPossumus.BaseTest.Builders.Settings;
using Xunit;

public class CurrencyApiManagerTest
{
	[Theory]
	[InlineData("USD", "ARS", 100)]
	[InlineData("ARS", "EUR", 10000)]
	public async Task Should_SendMultipleSmsAsync_Succeed(string from, string to, double amount)
	{
		// Arrange
		var appSettingHelper = new AppSettingHelperBuilder()
			.Setup()
			.Build();

		var sut = new CurrencyApiManager(appSettingHelper);

		// Act
		var act = await sut.GetNewAmountAsync(from, to, amount);

		// Assert
		act.Should().BeGreaterThan(default);
	}
}