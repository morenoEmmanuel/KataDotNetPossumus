namespace KataDotNetPossumus.Repository.Sql.Interfaces;

public interface ISqlRepository<TModel>
	where TModel : class
{
	Task CreateAsync(TModel entity);
	Task<TModel?> FindAsync(int id);
	Task SaveChangesAsync();
}