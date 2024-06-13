using KataDotNetPossumus.Business.Interfaces;
using KataDotNetPossumus.Exceptions;
using KataDotNetPossumus.Model.DataTransferObject.Authentication;
using KataDotNetPossumus.Model.Entities;
using KataDotNetPossumus.Repository.Sql.Interfaces;
using KataDotNetPossumus.Resources;
using KataDotNetPossumus.SettingHelper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KataDotNetPossumus.Business.Implementations;

public class AuthenticationBusiness : IAuthenticationBusiness
{
	#region Dependencies

	private readonly AppSettingHelper appSettingHelper;
	private readonly IUserRepository userRepository;

	#endregion

	#region Constructors

	public AuthenticationBusiness(
		AppSettingHelper appSettingHelper,
		IUserRepository userRepository)
	{
		this.appSettingHelper = appSettingHelper;
		this.userRepository = userRepository;
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// Authenticates the user's credentials, verifying the reCaptcha token.
	/// </summary>
	/// <param name="requestData">
	///		<para>The request data.</para>
	/// </param>
	/// <returns>The authenticated user's data.</returns>
	/// <exception cref="RequiredDataException">If the username was not receive.</exception>
	/// <exception cref="RequiredDataException">If the password was not receive.</exception>
	/// <exception cref="UnauthorizedAccessException">If the authentication failed.</exception>
	public async Task<DtoAuthenticationResponse?> AuthenticateAsync(DtoAuthenticationRequest? requestData)
	{
		if (string.IsNullOrWhiteSpace(requestData?.Username)) throw new RequiredDataException(Labels.Username);
		if (string.IsNullOrWhiteSpace(requestData.Password)) throw new RequiredDataException(Labels.Password);

		var user = await ValidateUserAsync(requestData.Username, requestData.Password);

		return new DtoAuthenticationResponse
		{
			Token = GenerateJwtToken(user)
		};
	}

	#endregion

	#region Private Methods

	private string GenerateJwtToken(User user)
	{
		var claims = new List<Claim>
		{
			new(JwtRegisteredClaimNames.Sub, user.Name),
			new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
			new(ClaimTypes.NameIdentifier, user.IdUser.ToString())
		};

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettingHelper.TokenSecret));
		var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
		var expires = DateTime.Now.AddHours(appSettingHelper.TokenExpirationHours);

		var token = new JwtSecurityToken(
			appSettingHelper.WebAppUrl,
			appSettingHelper.WebAppUrl,
			claims,
			expires: expires,
			signingCredentials: credentials
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}

	private async Task<User?> ValidateUserAsync(string username, string password)
	{
		var user = await userRepository.ValidateUserAsync(username, password);

		if (user == null) throw new UnauthorizedAccessException(Labels.Unauthorized);

		return user;
	}

	#endregion

}