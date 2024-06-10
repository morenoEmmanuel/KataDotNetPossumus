namespace KataDotNetPossumus.Repository.Sql.Interfaces;

public interface ISqlRepository<TModel>
	where TModel : class
{
	Task<List<TModel>> AllActiveAsync();
	Task<List<TModel>> AllAsync(string? sortColumn = null, string? sortOrder = null);
	Task<List<TModel>> AllNoTrackingAsync(string? sortColumn = null, string? sortOrder = null);
	Task CreateAsync(TModel entity);
	Task CreateRangeAsync(List<TModel> entities);
	Task<TModel?> DeleteAsync(int id, bool commitChanges = true);
	Task<TModel?> DeleteAsync(TModel? entity, bool commitChanges = true);
	Task DeleteListAsync(IEnumerable<TModel> list, bool commitChanges = false);
	Task<TModel?> DeleteLogicAsync(int id, bool commitChanges = true);
	Task<TModel?> DeleteLogicAsync(TModel? entity, bool commitChanges = true);
	Task<TModel?> FindAsync(int id);
	Task<TModel?> FirstAsync();
	Task<TModel?> FirstNoTrackingAsync();
	Task SaveChangesAsync();
}