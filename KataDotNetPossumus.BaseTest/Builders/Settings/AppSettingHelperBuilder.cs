using KataDotNetPossumus.SettingHelper;

namespace KataDotNetPossumus.BaseTest.Builders.Settings;

public class AppSettingHelperBuilder
{
	#region Services

	private AppSettingHelper entity;

	#endregion

	#region Options

	public AppSettingHelperBuilder Setup()
	{
		entity = StartupSettingHelper.GetSettingOptions();

		return this;
	}

	#endregion

	#region Build

	public AppSettingHelper Build()
	{
		return entity;
	}

	#endregion
}