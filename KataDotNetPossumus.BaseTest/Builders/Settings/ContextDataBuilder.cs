using KataDotNetPossumus.CurrentContext;

namespace KataDotNetPossumus.BaseTest.Builders.Settings;

public class ContextDataBuilder
{
	#region Services

	private readonly ContextData entity;

	#endregion

	#region Constructors

	public ContextDataBuilder()
	{
		entity = new ContextData();
	}

	#endregion

	#region Options
	
	public ContextDataBuilder SetupUserId(int userId)
	{
		entity.UserId = userId;

		return this;
	}

	#endregion

	#region Build

	public ContextData Build()
	{
		return entity;
	}

	#endregion
}