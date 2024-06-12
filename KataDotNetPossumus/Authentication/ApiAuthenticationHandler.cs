using KataDotNetPossumus.CurrentContext;
using KataDotNetPossumus.Resources;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Encodings.Web;

namespace KataDotNetPossumus.Api.Authentication;

public class ApiAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOptions>
{
	#region Dependencies

	private readonly ContextData contextData;

	#endregion

	#region Constructors

	public ApiAuthenticationHandler(
		ISystemClock clock,
		ContextData contextData,
		UrlEncoder encoder,
		ILoggerFactory logger,
		IOptionsMonitor<BasicAuthenticationOptions> options
		) : base(options, logger, encoder, clock)
	{
		this.contextData = contextData;
	}

	#endregion

	#region Overriden Methods

	protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
	{
		var token = GetToken(Request);

		if (string.IsNullOrWhiteSpace(token)) return AuthenticateResult.Fail(Labels.Unauthorized);

		contextData.Token = token;

		var tokenData = new JwtSecurityTokenHandler().ReadJwtToken(contextData.Token);

		var claims = new List<Claim>
		{
				new(ClaimTypes.Name, tokenData.Claims.First(p => p.Type == "sub").Value)
		};

		contextData.UserId = Convert.ToInt32(tokenData.Claims.First(p => p.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);

		var identity = new ClaimsIdentity(claims, Scheme.Name);
		var principal = new GenericPrincipal(identity, new[]
		{
				contextData.Username
		});

		var ticket = new AuthenticationTicket(principal, Scheme.Name);

		return AuthenticateResult.Success(ticket);
	}

	#endregion

	#region Private Methods
	
	private static string? GetToken(HttpRequest request)
	{
		if (!request.Headers.ContainsKey("Authorization")) return null;

		var authorizationHeader = request.Headers["Authorization"].ToString();

		if (string.IsNullOrWhiteSpace(authorizationHeader)) return null;

		return authorizationHeader.StartsWith(Labels.Bearer, StringComparison.OrdinalIgnoreCase)
			? authorizationHeader[Labels.Bearer.Length..].Trim()
			: null;
	}

	#endregion
}

public class BasicAuthenticationOptions : AuthenticationSchemeOptions
{
}