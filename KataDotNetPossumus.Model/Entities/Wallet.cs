using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KataDotNetPossumus.Model.Entities;

public class Wallet
{
	[Key]
	public int IdWallet { get; set; }

	public int IdUser { get; set; }
	public int Status { get; set; }

	#region Relationships to one

	[ForeignKey(nameof(IdUser))]
	public virtual User? User { get; set; }

	#endregion
}