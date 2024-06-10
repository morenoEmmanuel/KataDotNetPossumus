using KataDotNetPossumus.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KataDotNetPossumus.Api.Controllers
{

	[Route("api/[controller]")]
	public class AccessController : BaseController
	{
		#region Dependencies

		private readonly IAuthenticationBusiness authenticationBusiness;

		#endregion

		#region Constructors

		public AccessController(
			IAuthenticationBusiness authenticationBusiness)
		{
			this.authenticationBusiness = authenticationBusiness;
		}

		#endregion

		#region Endpoints
		

		/// <summary>
		/// Authenticates the user's credentials.
		/// </summary>
		/// <param name="requestData">
		///		<para>The request data.</para>
		/// </param>
		/// <returns>The authenticated user's data.</returns>
		[AllowAnonymous, HttpPost("authenticate")]
		public async Task<DtoAuthenticationResponse?> Authenticate([FromBody] DtoAuthenticationRequest? requestData)
		{
			return await authenticationBusiness.AuthenticateAsync(requestData);
		}

		#endregion
	}
}
