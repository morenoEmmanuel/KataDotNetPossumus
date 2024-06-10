using KataDotNetPossumus.CurrentContext;
using KataDotNetPossumus.Exceptions;
using KataDotNetPossumus.Model.CustomAttributes;
using KataDotNetPossumus.Model.SqlContext;
using KataDotNetPossumus.Repository.Sql.Interfaces;
using KataDotNetPossumus.Resources;
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
	/// Gets all the active entities.
	/// </summary>
	/// <returns>The list of all the active entities.</returns>
	public virtual async Task<List<TModel>> AllActiveAsync()
	{
		var type = typeof(TModel);
		var item = Expression.Parameter(type, "item");
		var equal = Expression.Equal(Expression.Property(item, type.GetStatusPropertyName() ?? "Status"), Expression.Constant(1));
		var conditions = Expression.Lambda<Func<TModel, bool>>(equal, item);

		return await WhereAsync(conditions);
	}

	/// <summary>
	/// Gets all the entities.
	/// </summary>
	/// <param name="sortColumn">
	///		<para>The column used to sort the entities.</para>
	/// </param>
	/// <param name="sortOrder">
	///		<para>The order direction used to sort the entities.</para>
	/// </param>
	/// <returns>The list of all the entities.</returns>
	public virtual async Task<List<TModel>> AllAsync(string? sortColumn = null, string? sortOrder = null)
	{
		var query = Queryable();

		if (!string.IsNullOrWhiteSpace(sortColumn))
		{
			query = ApplyOrder(query, sortColumn, sortOrder?.ToUpper() == "ASC" ? "OrderBy" : "OrderByDescending");
		}

		return await query.ToListAsync();
	}

	/// <summary>
	/// Gets all the entities, no tracking them in the DB context.
	/// </summary>
	/// <param name="sortColumn">
	///		<para>The column used to sort the entities.</para>
	/// </param>
	/// <param name="sortOrder">
	///		<para>The order direction used to sort the entities.</para>
	/// </param>
	/// <returns>The list of all the entities.</returns>
	public virtual async Task<List<TModel>> AllNoTrackingAsync(string? sortColumn = null, string? sortOrder = null)
	{
		var query = QueryableNoTracking();

		if (!string.IsNullOrWhiteSpace(sortColumn))
		{
			query = ApplyOrder(query, sortColumn, sortOrder?.ToUpper() == "ASC" ? "OrderBy" : "OrderByDescending");
		}

		return await query.ToListAsync();
	}

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
	/// Adds the received entities to the DB context.
	/// </summary>
	/// <param name="entities">
	///		<para>The entities.</para>
	/// </param>
	/// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
	public virtual async Task CreateRangeAsync(List<TModel> entities)
	{
		await dbContext.Set<TModel>().AddRangeAsync(entities);
	}

	/// <summary>
	/// Deletes an entity by its ID.
	/// </summary>
	/// <param name="id">
	///		<para>The entity ID.</para>
	/// </param>
	/// <param name="commitChanges">
	///		<para>Indicates if the changes must be committed in the DB.</para>
	/// </param>
	/// <returns>The deleted entity.</returns>
	public virtual async Task<TModel?> DeleteAsync(int id, bool commitChanges = true)
	{
		var entity = await FindAsync(id);

		return await DeleteAsync(entity, commitChanges);
	}

	/// <summary>
	/// Deletes the receive entity.
	/// </summary>
	/// <param name="entity">
	///		<para>The entity.</para>
	/// </param>
	/// <param name="commitChanges">
	///		<para>Indicates if the changes must be committed in the DB.</para>
	/// </param>
	/// <returns>The deleted entity.</returns>
	public virtual async Task<TModel?> DeleteAsync(TModel? entity, bool commitChanges = true)
	{
		if (entity is null) throw new NoNotifyException(Messages.EntityCouldNotBeDeleted);

		dbContext.Set<TModel>().Remove(entity);

		if (commitChanges)
		{
			await SaveChangesAsync();
		}

		return entity;
	}

	public virtual async Task DeleteListAsync(IEnumerable<TModel> list, bool commitChanges = false)
	{
		dbContext.Set<TModel>().RemoveRange(list);

		if (commitChanges)
		{
			await SaveChangesAsync();
		}
	}

	/// <summary>
	/// Deletes logically an entity by its ID.
	/// </summary>
	/// <param name="id">
	///		<para>The entity ID.</para>
	/// </param>
	/// <param name="commitChanges">
	///		<para>Indicates if the changes must be committed in the DB.</para>
	/// </param>
	/// <returns>The deleted entity.</returns>
	public virtual async Task<TModel?> DeleteLogicAsync(int id, bool commitChanges = true)
	{
		var entity = await FindAsync(id);

		return await DeleteLogicAsync(entity, commitChanges);
	}

	/// <summary>
	/// Deletes logically the receive entity.
	/// </summary>
	/// <param name="entity">
	///		<para>The entity.</para>
	/// </param>
	/// <param name="commitChanges">
	///		<para>Indicates if the changes must be committed in the DB.</para>
	/// </param>
	/// <returns>The deleted entity.</returns>
	public virtual async Task<TModel?> DeleteLogicAsync(TModel? entity, bool commitChanges = true)
	{
		if (entity == null) throw new NoNotifyException(Messages.EntityCouldNotBeDeleted);

		var id = GetEntityId(entity);

		if (id is null) return null;

		var type = typeof(TModel);

		// Sets edition date property if exists
		var editionDateProperty = type.GetProperty("EditionDate");

		if (editionDateProperty != null)
		{
			editionDateProperty.SetValue(entity, DateTime.UtcNow, null);
		}

		// Sets status property
		var statusPropertyName = type.GetStatusPropertyName() ?? "Status";
		var statusProperty = type.GetProperty(statusPropertyName);

		if (statusProperty == null) throw new MissingPropertyException(statusPropertyName);

		var statusValue = statusProperty.GetValue(entity) ?? (int)Enumerations.Enumerations.EntityStatus.DELETED;

		if (Convert.ToInt32(statusValue) == (int)Enumerations.Enumerations.EntityStatus.ACTIVE)
		{
			statusProperty.SetValue(entity, (int)Enumerations.Enumerations.EntityStatus.DELETED, null);
		}

		if (commitChanges)
		{
			await SaveChangesAsync();
		}

		return entity;
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
	/// Gets the first entity in the DB.
	/// </summary>
	/// <returns>The first entity in the DB.</returns>
	public virtual async Task<TModel?> FirstAsync()
	{
		return await Queryable().FirstOrDefaultAsync();
	}

	/// <summary>
	/// Gets the first entity in the DB, no tracking it in the DB context.
	/// </summary>
	/// <returns>The first entity in the DB.</returns>
	public virtual async Task<TModel?> FirstNoTrackingAsync()
	{
		return await QueryableNoTracking().FirstOrDefaultAsync();
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

	protected async Task<List<TU>> AllSelectAsync<TU>(Expression<Func<TModel, TU>> columns)
	{
		return await Queryable().Select(columns).ToListAsync();
	}

	protected async Task<bool> AnyAsync(Expression<Func<TModel, bool>> conditions)
	{
		return await Queryable(conditions).AnyAsync();
	}
	
	protected async Task<int> CountAsync(Expression<Func<TModel, bool>> conditions)
	{
		return await Queryable(conditions).CountAsync();
	}
	
	protected async Task<TModel?> FindAsync(Expression<Func<TModel, bool>> conditions, params Expression<Func<TModel, object>>[] includes)
	{
		return await Queryable(conditions, includes).FirstOrDefaultAsync();
	}

	protected async Task<TModel?> FindNoTrackingAsync(Expression<Func<TModel, bool>> conditions, params Expression<Func<TModel, object>>[] includes)
	{
		return await QueryableNoTracking(conditions, includes).FirstOrDefaultAsync();
	}

	protected async Task<TU?> FindSelectAsync<TU>(Expression<Func<TModel, bool>> conditions, Expression<Func<TModel, TU>> columns)
	{
		return await Queryable(conditions).Select(columns).FirstOrDefaultAsync();
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

	protected async Task<List<TModel>> WhereAsync(Expression<Func<TModel, bool>> conditions, params Expression<Func<TModel, object>>[] includes)
	{
		return await Queryable(conditions, includes).ToListAsync();
	}

	protected async Task<List<TModel>> WhereNoTrackingAsync(Expression<Func<TModel, bool>> conditions, params Expression<Func<TModel, object>>[] includes)
	{
		return await QueryableNoTracking(conditions, includes).ToListAsync();
	}

	protected async Task<List<TU>> WhereSelectAsync<TU>(Expression<Func<TModel, bool>> conditions, Expression<Func<TModel, TU>> columns)
	{
		return await Queryable(conditions).Select(columns).ToListAsync();
	}

	protected async Task<List<TU>> WhereOrderSelectAsync<TU>(Expression<Func<TModel, bool>> conditions, Expression<Func<TModel, TU>> columns, string sortColumn, string? sortOrder = "ASC")
	{
		var query = Queryable(conditions);

		if (!string.IsNullOrWhiteSpace(sortColumn))
		{
			query = ApplyOrder(query, sortColumn, sortOrder?.ToUpper() == "ASC" ? "OrderBy" : "OrderByDescending");
		}

		return await query.Select(columns).ToListAsync();
	}

	#endregion

	#region Private Methods

	private static IOrderedQueryable<TModel> ApplyOrder(IQueryable<TModel> source, string property, string methodName)
	{
		var props = property.Split('.');
		var type = typeof(TModel);
		var arg = Expression.Parameter(type, "x");
		Expression expr = arg;

		foreach (var prop in props)
		{
			// Uses reflection (not ComponentModel) to mirror LINQ
			var pi = type.GetProperty(prop);

			if (pi == null) continue;

			expr = Expression.Property(expr, pi);
			type = pi.PropertyType;
		}

		var delegateType = typeof(Func<,>).MakeGenericType(typeof(TModel), type);
		var lambda = Expression.Lambda(delegateType, expr, arg);

		var result = typeof(Queryable).GetMethods().Single(method =>
				method.Name == methodName
				&& method.IsGenericMethodDefinition
				&& method.GetGenericArguments().Length == 2
				&& method.GetParameters().Length == 2)
			.MakeGenericMethod(typeof(TModel), type)
			.Invoke(null, new object[]
			{
					source,
					lambda
			});

		return (IOrderedQueryable<TModel>)result!;
	}
	
	private object? GetEntityId(TModel entity)
	{
		var type = entity.GetType();

		var keyName = dbContext.Model
			.FindEntityType(type)?
			.FindPrimaryKey()?.Properties
			.Select(x => x.Name)
			.Single();

		if (string.IsNullOrWhiteSpace(keyName)) return null;

		return type.GetProperty(keyName)?.GetValue(entity, null);
	}

	#endregion
}