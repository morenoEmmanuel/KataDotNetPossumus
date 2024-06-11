using KataDotNetPossumus.Resources;

namespace KataDotNetPossumus.Exceptions;

public class NotFoundException : Exception
{
	public NotFoundException(string entityName)
		: base(string.Format(Messages.EntityNotFoundFormat, entityName))
	{
	}

	public NotFoundException(string entityName, string? value)
		: base(string.Format(Messages.NamedEntityNotFoundFormat, entityName, value))
	{
	}
}