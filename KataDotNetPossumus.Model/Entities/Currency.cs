using System.ComponentModel.DataAnnotations;

namespace KataDotNetPossumus.Model.Entities;

public class Currency
{
	[Key]
	public int IdCurrency { get; set; }

	public string ShortName { get; set; } = string.Empty;
	public int Status { get; set; }
}