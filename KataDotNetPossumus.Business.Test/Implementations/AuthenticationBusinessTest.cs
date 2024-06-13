using FluentAssertions;
using KataDotNetPossumus.BaseTest.Builders.Repository;
using KataDotNetPossumus.BaseTest.Builders.Settings;
using KataDotNetPossumus.Business.Implementations;
using KataDotNetPossumus.Exceptions;
using KataDotNetPossumus.Model.DataTransferObject.Authentication;
using KataDotNetPossumus.Model.Entities;
using KataDotNetPossumus.Resources;
using Xunit;

namespace KataDotNetPossumus.BaseTest.Builders.Business;

public class AuthenticationBusinessTest
{
	#region Tests

	[Fact]
	public async Task Should_Authenticate_Fail_UserNameRequired()
	{
		// Arrange
		var sut = new AuthenticationBusiness(
			null,
			null);

		// Act
		Task Act() => sut.AuthenticateAsync(new DtoAuthenticationRequest
		{
			Password = "123"
		});

		// Assert
		var exception = await Assert.ThrowsAsync<RequiredDataException>(Act);

		exception.Message.Should().Be(string.Format(Messages.RequiredFieldFormat, Labels.Username));
	}

	[Fact]
	public async Task Should_Authenticate_Fail_PasswordRequired()
	{
		// Arrange
		var sut = new AuthenticationBusiness(
			null,
			null);

		// Act
		Task Act() => sut.AuthenticateAsync(new DtoAuthenticationRequest
		{
			Username = "123"
		});

		// Assert
		var exception = await Assert.ThrowsAsync<RequiredDataException>(Act);

		exception.Message.Should().Be(string.Format(Messages.RequiredFieldFormat, Labels.Password));
	}

	[Fact]
	public async Task Should_Authenticate_Fail_UserNotFound()
	{
		// Arrange
		var userRepository = new UserRepositoryBuilder()
			.SetupMethodValidateUser(null)
			.Build();

		var sut = new AuthenticationBusiness(
			null,
			userRepository);

		// Act
		Task Act() => sut.AuthenticateAsync(new DtoAuthenticationRequest
		{
			Username = "123",
			Password = "123"
		});

		// Assert
		var exception = await Assert.ThrowsAsync<UnauthorizedAccessException>(Act);

		exception.Message.Should().Be(Labels.Unauthorized);
	}

	[Fact]
	public async Task Should_Authenticate_Succeed()
	{
		// Arrange
		var appSettingHelper = new AppSettingHelperBuilder()
			.Setup()
			.Build();

		var userRepository = new UserRepositoryBuilder()
			.SetupMethodValidateUser(new User())
			.Build();

		var sut = new AuthenticationBusiness(
			appSettingHelper,
			userRepository);

		// Act
		var act = await sut.AuthenticateAsync(new DtoAuthenticationRequest
		{
			Username = "123",
			Password = "123"
		});

		// Assert
		act?.Token.Should().NotBeNull();
	}

	#endregion
}