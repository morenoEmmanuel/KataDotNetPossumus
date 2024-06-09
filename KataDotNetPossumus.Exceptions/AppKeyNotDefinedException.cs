using KataDotNetPossumus.Resources;

namespace KataDotNetPossumus.Exceptions;

public class AppKeyNotDefinedException : Exception
{
	public AppKeyNotDefinedException(string keyName)
		: base(string.Format(Messages.AppKeyNotDefined, keyName))
	{
	}
}