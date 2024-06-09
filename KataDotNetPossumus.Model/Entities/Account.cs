using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KataDotNetPossumus.Model.Entities;

public class Account
{
	[Key]
	public int IdAccount { get; set; }

	public int IdWallet { get; set; }
	public int IdCurrency { get; set; }
	public double Balance { get; set; }
	public int Status { get; set; }

	#region Relationships to one
	
	[ForeignKey(nameof(IdWallet))]
	public virtual Wallet? Wallet { get; set; }

	[ForeignKey(nameof(IdCurrency))]
	public virtual Currency? Currency { get; set; }

	#endregion

}