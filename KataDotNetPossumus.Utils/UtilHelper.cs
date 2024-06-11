using KataDotNetPossumus.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;
using System.Text.Json;

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
				//case EntityCannotBeDeletedException:
				//case EntityExistsException:
				case NoNotifyException:
				case RequiredDataException:
				//case ServiceRequestException:
					return StatusCodes.Status400BadRequest;
				//case NotFoundException:
				//	return StatusCodes.Status404NotFound;
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