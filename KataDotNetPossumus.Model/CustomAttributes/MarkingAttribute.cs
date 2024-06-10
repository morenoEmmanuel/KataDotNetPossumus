using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace KataDotNetPossumus.Model.CustomAttributes;

public class BaseModelNamePropertyAttribute : Attribute
{
	public string Property { get; set; }

	public BaseModelNamePropertyAttribute(string? propertyName = null)
	{
		Property = propertyName ?? string.Empty;
	}
}

[AttributeUsage(AttributeTargets.Property)]
public class StatusPropertyAttribute : BaseModelNamePropertyAttribute
{
	public StatusPropertyAttribute([CallerMemberName] string? propertyName = null)
		: base(propertyName)
	{
	}
}

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
public class ExcludeFromAuditAttribute : BaseModelNamePropertyAttribute
{
	public ExcludeFromAuditAttribute([CallerMemberName] string? propertyName = null)
		: base(propertyName)
	{
	}
}

public static class TypeAttributeExtension
{
	public static List<string> GetNameOfProperties<TAttribute>(Type type, string property)
	{
		var attrNames = new List<string>();

		var att = type.GetCustomAttributes(typeof(MetadataTypeAttribute), true).FirstOrDefault();

		var props = (att as MetadataTypeAttribute)?.MetadataClassType
			.GetProperties(BindingFlags.Public | BindingFlags.Instance)
			.Where(p => p.GetCustomAttributes(typeof(TAttribute), false).Count() == 1);

		if (props == null) return attrNames;

		foreach (var prop in props)
		{
			var temp = (TAttribute)prop.GetCustomAttributes(typeof(TAttribute), false)[0];

			var member = temp?.GetType().InvokeMember(property, BindingFlags.GetProperty, null, temp, null);

			if (member is not null)
			{
				attrNames.Add(member.ToString() ?? string.Empty);
			}
		}

		return attrNames;
	}

	public static string? GetNameOfPropertyName<TAttribute>(Type type, string property)
	{
		var att = type.GetCustomAttributes(typeof(MetadataTypeAttribute), true).FirstOrDefault();

		if (att == null) return null;

		var prop = (att as MetadataTypeAttribute)?.MetadataClassType
			.GetProperties(BindingFlags.Public | BindingFlags.Instance)
			.FirstOrDefault(p => p.GetCustomAttributes(typeof(TAttribute), false).Length == 1);

		if (prop == null) return null;

		var temp = (TAttribute)prop.GetCustomAttributes(typeof(TAttribute), false)[0];

		return temp?.GetType()
			.InvokeMember(property, BindingFlags.GetProperty, null, temp, null)?
			.ToString();
	}

	public static string? GetStatusPropertyName(this Type type)
	{
		return GetNameOfPropertyName<StatusPropertyAttribute>(type, "Property");
	}
}