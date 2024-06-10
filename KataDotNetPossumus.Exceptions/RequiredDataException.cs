using KataDotNetPossumus.Resources;

namespace KataDotNetPossumus.Exceptions;

public class RequiredDataException : Exception
{
	public RequiredDataException(string fieldName)
		: base(string.Format(Messages.RequiredFieldFormat, fieldName))
	{
	}

	public RequiredDataException(IEnumerable<string> fieldNames)
		: base(string.Format(Messages.RequiredFieldsFormat, $"'{string.Join("', '", fieldNames)}'"))
	{
	}
}