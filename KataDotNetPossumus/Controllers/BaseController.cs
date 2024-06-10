using KataDotNetPossumus.Enumerations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KataDotNetPossumus.Api.Controllers
{
	[ApiController, Authorize(AuthenticationSchemes = AuthenticationSchemes.Bearer)]
	public class BaseController : ControllerBase
	{
		#region Private Methods 

		protected void AddErrorsToModelState(Dictionary<string, string> errors)
		{
			foreach (var (key, value) in errors)
			{
				ModelState.AddModelError(key, value);
			}
		}

		private static string? GetFormData()
		{
			return null;
		}

		private string? GetQueryString()
		{
			var queryParts = Request
				.QueryString
				.Value?
				.TrimStart('?')
				.Split('&');

			if (queryParts is null) return null;

			var queryString = string.Join(" | ", from string query in queryParts select query);

			return !string.IsNullOrWhiteSpace(queryString)
				? queryString
				: null;
		}

		private static string? GetValidationErrors(/*Exception ex*/)
		{
			return null;
		}

		#endregion
	}
}
