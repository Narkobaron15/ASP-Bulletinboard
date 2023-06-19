using System.Linq.Expressions;

namespace Repository.Interfaces;

/// <summary>
/// CRUD interface
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IGenericRepository<TEntity> : IGenericSTRepository<TEntity>, IGenericAsyncRepository<TEntity>
    where TEntity : class
{ }

/// <summary>
/// Single threaded operations CRUD interface
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IGenericSTRepository<TEntity> where TEntity : class
{
    // Create
    void Add(TEntity entity);
    void AddRange(IEnumerable<TEntity> entities);
    // Read

    // Read single item
    TEntity? FindById(params object?[]? id);

    // Read all entities
    IEnumerable<TEntity> GetAll();
    IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> _pred);

    // Specific read tasks
    public bool Contains(TEntity entity);

    // Update
    void Update(TEntity entity);
    void UpdateRange(IEnumerable<TEntity> entities);

    // Delete
    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);
}

/// <summary>
/// Multi threaded operations CRUD interface
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IGenericAsyncRepository<TEntity> where TEntity : class
{
    // Create
    Task AddAsync(TEntity entity);
    Task AddRangeAsync(IEnumerable<TEntity> entities);

    // Read

    // Read single item
    Task<TEntity?> FindByIdAsync(params object?[]? id);

    // Read all entities
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> _pred);
    public Task<IEnumerable<TEntity>> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties);
    //public Task<IEnumerable<TEntity>> GetWithInclude(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includeProperties);

    // Specific read tasks
    public Task<bool> ContainsAsync(TEntity entity);

    // Update
    Task UpdateAsync(TEntity item);
    Task UpdateRangeAsync(IEnumerable<TEntity> entities);

    // Delete
    Task RemoveAsync(TEntity item);
    Task RemoveRangeAsync(IEnumerable<TEntity> entities);
}