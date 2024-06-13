namespace KataDotNetPossumus.Exceptions
{
	public class BadRequestException : Exception
	{
		public BadRequestException(string message)
			: base(message)
		{
		}

		public BadRequestException(string messageFormat, string message)
			: base(string.Format(messageFormat, message))
		{
		}
	}
}