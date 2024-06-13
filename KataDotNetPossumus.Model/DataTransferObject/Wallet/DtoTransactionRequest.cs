namespace KataDotNetPossumus.Model.DataTransferObject.Wallet;

public class DtoTransactionRequest
{
	public string? CurrentCurrency { get; set; }
	public double? Amount { get; set; }
}