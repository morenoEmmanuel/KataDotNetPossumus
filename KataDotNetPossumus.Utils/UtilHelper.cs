using KataDotNetPossumus.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KataDotNetPossumus.Utils
{
	public static class UtilHelper
	{
		#region Public Methods

		public static int GetErrorCode(Exception? exception)
		{
			switch (exception)
			{
				case BadRequestException:
				case NoNotifyException:
				case RequiredDataException:
					return StatusCodes.Status400BadRequest;
				case NotFoundException:
					return StatusCodes.Status404NotFound;
				case UnauthorizedAccessException:
					return StatusCodes.Status401Unauthorized;
				default:
					return StatusCodes.Status500InternalServerError;
			}
		}

		public static string? Serialize(object? value, bool ignoreNull = false)
		{
			return value == null
				? null
				: JsonSerializer.Serialize(value, new JsonSerializerOptions
				{
					DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
				});
		}
		#endregion
	}
}