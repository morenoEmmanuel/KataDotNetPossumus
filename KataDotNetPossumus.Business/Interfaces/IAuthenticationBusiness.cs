using System.Text.Json;
using System.Text.Json.Serialization;

namespace KataDotNetPossumus.Business.Interfaces;

public interface IAuthenticationBusiness
{
	Task<DtoAuthenticationResponse?> AuthenticateAsync(DtoAuthenticationRequest? requestData);
}

//TODO: MOVER ESTA CLASE A LA CARPETA CORRESPONDIENTE
public class DtoAuthenticationRequest : DtoSelfSerializable
{
	public string? Username { get; set; }
	public string? Password { get; set; }

	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public bool? IncludeRefreshToken { get; set; }
}

//TODO: MOVER ESTA CLASE A LA CARPETA CORRESPONDIENTE
public class DtoSelfSerializable
{
	#region Public Methods

	public virtual string? ToString(bool ignoreNull = false)
	{
		return Serialize(this, ignoreNull);
	}

	public Dictionary<string, string?> ToDictionary(bool usePropertyNames = true, bool? ignoreNull = false)
	{
		var properties = GetType()
			.GetProperties()
			.Where(p => p.PropertyType.IsPublic)
			.ToList();

		var dictionary = new Dictionary<string, string?>();

		properties.ForEach(p =>
		{
			var name = p.Name;

			if (usePropertyNames)
			{
				var jsonProperty = p.CustomAttributes
					.FirstOrDefault(q => q.AttributeType.Name == nameof(JsonPropertyNameAttribute));

				if (jsonProperty != null)
				{
					var propertyValue = jsonProperty
						.ConstructorArguments
						.FirstOrDefault(q => q.Value != null)
						.Value?
						.ToString();

					if (!string.IsNullOrWhiteSpace(propertyValue))
					{
						name = propertyValue;
					}
				}
			}

			dictionary.Add(name, p.GetValue(this)?.ToString());
		});

		if (ignoreNull is true)
		{
			foreach (var item in dictionary.Where(o => o.Value is null))
			{
				dictionary.Remove(item.Key);
			}
		}

		return dictionary;
	}

	#endregion



	//TODO: MOVER ESTA CLASE A LA CARPETA CORRESPONDIENTE
	public static string? Serialize(object? value, bool ignoreNull = false)
	{
		return value == null
			? null
			: JsonSerializer.Serialize(value, new JsonSerializerOptions
			{
				DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
			});
	}
}

//TODO: MOVER ESTA CLASE A LA CARPETA CORRESPONDIENTE
public class DtoAuthenticationResponse
{
	public string Token { get; set; } = string.Empty;
}