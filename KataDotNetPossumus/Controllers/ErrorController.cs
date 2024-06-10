using KataDotNetPossumus.Exceptions;
using KataDotNetPossumus.Resources;
using KataDotNetPossumus.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace KataDotNetPossumus.Api.Controllers
{
	[Route("api/[controller]")]
	public class ErrorController : BaseController
	{
		#region Endpoints

		[AllowAnonymous, ApiExplorerSettings(IgnoreApi = true), Route("/error")]
		public async Task<IActionResult> Error()
		{
			var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

			GetErrorData(context?.Error, out var message, out var errorCode);

			var path = context is not null
				? ((ExceptionHandlerFeature)context).Path
				: string.Empty;
			
			return Problem(
				message,
				statusCode: errorCode,
				title: Messages.Error);
		}

		#endregion

		#region Private Methods

		private static void GetErrorData(Exception? ex, out string message, out int errorCode)
		{
			message = ex?.Message ?? string.Empty;
			errorCode = UtilHelper.GetErrorCode(ex);

			switch (ex)
			{
				case BadRequestException:
				//case EntityCannotBeDeletedException:
				//case EntityExistsException:
				case NoNotifyException:
				//case NotFoundException:
				case RequiredDataException:
				//case ServiceRequestException:
				case UnauthorizedAccessException:
					break;
				default:
					message = Messages.UnexpectedError;
					break;
			}
		}
		#endregion
	}
}
