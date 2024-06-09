using KataDotNetPossumus.Exceptions;
using Microsoft.Extensions.Options;

namespace KataDotNetPossumus.SettingHelper
{
	public class AppSettingHelper
	{
		#region Dependencies

		private readonly AppSettingOptions appSettingsOptions = new();

		#endregion

		#region Constructors

		public AppSettingHelper(IOptions<AppSettingOptions> options)
		{
			SetAppSettings(options.Value);
		}

		#endregion

		#region Keys
		
		#region Local Encryption

		public string LocalEncryptionKey => GetLocalKeyValue("LocalEncryptionKey");
		public string LocalEncryptionSaltKey => GetLocalKeyValue("LocalEncryptionSaltKey");
		public string LocalEncryptionViKey => GetLocalKeyValue("LocalEncryptionViKey");

		#endregion

		#region Currency API

		public string CurrencyApiUrl => GetLocalKeyValue("CurrencyApiUrl");
		public string CurrencyKey => GetLocalKeyValue("CurrencyKey");

		#endregion

		#endregion

		#region Private Methods
		
		private string GetAppSettingValue(string keyName, bool required, string? alternativeKeyName = null)
		{
			if (appSettingsOptions.Keys.ContainsKey(keyName))
			{
				return appSettingsOptions.Keys[keyName].ToString() ?? string.Empty;
			}

			if (!string.IsNullOrWhiteSpace(alternativeKeyName))
			{
				if (appSettingsOptions.Keys.ContainsKey(alternativeKeyName))
				{
					return appSettingsOptions.Keys[alternativeKeyName].ToString() ?? string.Empty;
				}
			}

			if (required)
			{
				throw new AppKeyNotDefinedException(keyName);
			}

			return string.Empty;
		}
		
		private string GetLocalKeyValue(string keyName, bool required = true, string? alternativeKeyName = null)
		{
			return GetAppSettingValue(keyName, required, alternativeKeyName);
		}

		private void SetAppSettings(AppSettingOptions appSettings)
		{
			foreach (var (key, value) in appSettings.Keys)
			{
				appSettingsOptions.Keys.Add(key, value);
			}
		}

		#endregion
	}

	public class AppSettingOptions
	{
		public Dictionary<string, object> Keys { get; set; } = new();
	}
}