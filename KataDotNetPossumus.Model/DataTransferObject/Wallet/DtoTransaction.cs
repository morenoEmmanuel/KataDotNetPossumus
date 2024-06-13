namespace KataDotNetPossumus.Model.DataTransferObject.Wallet;

public class DtoTransaction
{
	public int IdWallet { get; set; }
	public int IdAccount { get; set; }
	public int IdCurrency { get; set; }
	public int IdNewCurrency { get; set; }
	public double TransactionAmount { get; set; }
}