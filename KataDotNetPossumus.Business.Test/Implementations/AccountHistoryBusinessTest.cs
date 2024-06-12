using KataDotNetPossumus.BaseTest.Builders.Repository;
using KataDotNetPossumus.Business.Implementations;
using KataDotNetPossumus.Model.Entities;
using Xunit;

namespace KataDotNetPossumus.BaseTest.Builders.Business;

public class AccountHistoryBusinessTest
{
	#region Test

	[Fact]
	public async Task Should_GetAccountsByWallet_Succeed()
	{
		// Arrange
		var accountHistoryRepository = new AccountHistoryRepositoryBuilder()
			.SetupMethodCreate()
			.Build();

		var sut = new AccountHistoryBusiness(accountHistoryRepository);

		// Act
		 await sut.SaveAccountHistoryAsync(new AccountHistory());
	}

	#endregion
}