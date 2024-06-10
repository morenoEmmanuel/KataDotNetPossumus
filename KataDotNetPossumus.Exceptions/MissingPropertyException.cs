using KataDotNetPossumus.Resources;

namespace KataDotNetPossumus.Exceptions;

public class MissingPropertyException : Exception
{
	public MissingPropertyException(string propertyName)
		: base(string.Format(Messages.MissingPropertyFormat, propertyName))
	{
	}
}