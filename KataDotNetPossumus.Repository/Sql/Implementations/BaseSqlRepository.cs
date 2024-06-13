using KataDotNetPossumus.CurrentContext;
using KataDotNetPossumus.Model.SqlContext;
using KataDotNetPossumus.Repository.Sql.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KataDotNetPossumus.Repository.Sql.Implementations;

public abstract class BaseSqlRepository<TModel> : ISqlRepository<TModel>
	where TModel : class
{
	#region Dependencies
	
	protected readonly ContextData ContextData;
	private readonly KataDotNetPossumusDbContext dbContext;

	#endregion

	#region Costructor

	protected BaseSqlRepository(
		ContextData contextData,
		KataDotNetPossumusDbContext dbContext)
	{
		ContextData = contextData;
		this.dbContext = dbContext;
	}

	#endregion

	#region Public Methods
	
	/// <summary>
	/// Adds the received entity to the DB context.
	/// </summary>
	/// <param name="entity">
	///		<para>The entity.</para>
	/// </param>
	/// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
	public virtual async Task CreateAsync(TModel entity)
	{
		await dbContext.Set<TModel>().AddAsync(entity);
	}
	
	/// <summary>
	/// Gets an entity by its ID.
	/// </summary>
	/// <param name="id">
	///		<para>The entity ID.</para>
	/// </param>
	/// <returns>The entity.</returns>
	public virtual async Task<TModel?> FindAsync(int id)
	{
		var query = dbContext.Set<TModel>();

		return await query.FindAsync(id);
	}
	
	/// <summary>
	/// Commits the changes in the DB.
	/// </summary>
	/// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
	public async Task SaveChangesAsync()
	{
		await dbContext.SaveChangesAsync();
	}

	#endregion

	#region Protected Methods
	
	protected async Task<TModel?> FindAsync(Expression<Func<TModel, bool>> conditions, params Expression<Func<TModel, object>>[] includes)
	{
		return await Queryable(conditions, includes).FirstOrDefaultAsync();
	}

	protected async Task<TModel?> FindNoTrackingAsync(Expression<Func<TModel, bool>> conditions, params Expression<Func<TModel, object>>[] includes)
	{
		return await QueryableNoTracking(conditions, includes).FirstOrDefaultAsync();
	}
	
	protected IQueryable<TModel> Queryable(Expression<Func<TModel, bool>>? conditions = null, params Expression<Func<TModel, object>>[]? includes)
	{
		var query = dbContext.Set<TModel>().AsQueryable();

		if (conditions is not null)
		{
			query = query.Where(conditions);
		}

		if (includes is not null && includes.Any())
		{
			query = includes.Aggregate(query, (current, include) => current.Include(include));
		}

		return query;
	}

	protected IQueryable<TModel> QueryableNoTracking(Expression<Func<TModel, bool>>? conditions = null, params Expression<Func<TModel, object>>[] includes)
	{
		return Queryable(conditions, includes).AsNoTracking();
	}
	
	#endregion
}