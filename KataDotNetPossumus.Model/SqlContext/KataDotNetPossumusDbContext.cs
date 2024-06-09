using KataDotNetPossumus.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace KataDotNetPossumus.Model.SqlContext;

public class KataDotNetPossumusDbContext : DbContext
{
	#region Entity Sets
	
	public DbSet<Account> Account { get; set; }
	public DbSet<AccountHistory> AccountHistory { get; set; }
	public DbSet<Currency> Currency { get; set; }
	public DbSet<User> User { get; set; }
	public DbSet<Wallet> Wallet { get; set; }

	#endregion
}