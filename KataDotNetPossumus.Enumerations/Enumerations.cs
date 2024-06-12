namespace KataDotNetPossumus.Enumerations;

public class Enumerations
{
	public enum EntityStatus
	{
		DELETED = 0,
		ACTIVE = 1
	}

	public enum TransactionType
	{
		DEPOSIT = 1,
		WITHDRAW = 2,
		EXCHANGE = 3
	}
}