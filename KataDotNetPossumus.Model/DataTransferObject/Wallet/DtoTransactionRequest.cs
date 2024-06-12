namespace KataDotNetPossumus.Model.DataTransferObject.Wallet;

public class DtoTransactionRequest
{
	public int? IdWallet { get; set; }
	public string? CurrentCurrency { get; set; }
	public double? Amount { get; set; }
}