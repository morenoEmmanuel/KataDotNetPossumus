using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KataDotNetPossumus.Model.Entities;

public class AccountHistory
{
	[Key]
	public int IdAccountHistory { get; set; }

	public int IdAccount { get; set; }
	public int EditedBy { get; set; }
	public double NewBalance { get; set; }
	public double OldBalance { get; set; }
	public DateTime EditionDate { get; set; }


	#region Relationships to one

	[ForeignKey(nameof(IdAccount))]
	public virtual Account? Account { get; set; }

	[ForeignKey(nameof(EditedBy))]
	public virtual User? User { get; set; }

	#endregion
}