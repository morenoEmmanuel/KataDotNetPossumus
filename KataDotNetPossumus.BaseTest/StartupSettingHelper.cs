using KataDotNetPossumus.SettingHelper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace KataDotNetPossumus.BaseTest
{
	public class StartupSettingHelper
	{
		#region Properties

		private const string AppSettingsFileName = "appsettings.json";

		#endregion

		#region Public Methods

		public static AppSettingHelper GetSettingOptions()
		{
			var settingsPath = GetSettingFilesDirectory();

			var configurations = new ConfigurationBuilder()
				.SetBasePath(settingsPath)
				.AddJsonFile(AppSettingsFileName)
				.Build();

			var appSettingOptions = new AppSettingOptions();
			var options = Options.Create(appSettingOptions);

			SetAppSettings(configurations, appSettingOptions);

			return new AppSettingHelper(options);
		}

		#endregion

		#region Private Methods

		private static bool ContainsAppSettingsFile(string path)
		{
			if (!Directory.Exists(path)) return false;

			var files = Directory.GetFiles(path);

			files = files
				.Select(p => p.Split('\\').Last())
				.ToArray();

			return files.Contains(AppSettingsFileName);
		}

		private static string GetSettingFilesDirectory()
		{
			var path = Directory.GetCurrentDirectory();

			do
			{
				if (ContainsAppSettingsFile(path))
				{
					break;
				}

				var apiPath = $@"{path}\KataDotNetPossumus";

				if (ContainsAppSettingsFile(apiPath))
				{
					path = apiPath;

					break;
				}

				path = Directory.GetParent(path)?.FullName;
			} while (path?.Split('\\').Length > 1);

			return path ?? string.Empty;
		}

		private static void SetAppSettings(IConfiguration configuration, AppSettingOptions settings)
		{
			// Gets the section "AppSettings" from the config file
			var appSettingsSection = configuration.GetSection("AppSettings");

			// Iterates for each setting key
			foreach (var generalSetting in appSettingsSection.GetChildren())
			{
				if (generalSetting.Value == null) continue;

				settings.Keys.Add(generalSetting.Key, generalSetting.Value);
			}
		}

		#endregion
	}
}