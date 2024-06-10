using KataDotNetPossumus.Business.Interfaces;

namespace KataDotNetPossumus.Business.Implementations;

public class AuthenticationBusiness : IAuthenticationBusiness
{
	/// <summary>
	/// Authenticates the user's credentials, verifying the reCaptcha token.
	/// </summary>
	/// <param name="requestData">
	///		<para>The request data.</para>
	/// </param>
	/// <returns>The authenticated user's data.</returns>
	/// <exception cref="RequiredDataException">If the username was not receive.</exception>
	/// <exception cref="RequiredDataException">If the password was not receive.</exception>
	/// <exception cref="UnauthorizedAccessException">If the Jarvis authentication failed.</exception>
	/// <exception cref="UnauthorizedAccessException">If the user is not a merchant.</exception>
	public async Task<DtoAuthenticationResponse?> AuthenticateAsync(DtoAuthenticationRequest? requestData)
	{
		//TODO: completar logica
		//if (string.IsNullOrWhiteSpace(requestData?.Username)) throw new RequiredDataException(Labels.Username);
		//if (string.IsNullOrWhiteSpace(requestData.Password)) throw new RequiredDataException(Labels.Password);

		return new DtoAuthenticationResponse();

		//return responseData;
	}
}