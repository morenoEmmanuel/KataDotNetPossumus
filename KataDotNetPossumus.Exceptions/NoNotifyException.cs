namespace KataDotNetPossumus.Exceptions;

public class NoNotifyException : Exception
{
	public NoNotifyException(Exception innerException, string message)
		: base(message, innerException)
	{
	}

	public NoNotifyException(string message)
		: base(message)
	{
	}

	public NoNotifyException(string messageFormat, params object[] parameters)
		: base(string.Format(messageFormat, parameters))
	{
	}

	public NoNotifyException(string messageFormat, string? message)
		: base(string.Format(messageFormat, message))
	{
	}
}