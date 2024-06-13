using KataDotNetPossumus.Enumerations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KataDotNetPossumus.Api.Controllers
{
	[ApiController, Authorize(AuthenticationSchemes = AuthenticationSchemes.Bearer)]
	public class BaseController : ControllerBase
	{
	}
}
