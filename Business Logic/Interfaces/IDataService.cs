using System.Linq.Expressions;

namespace Business_Logic.Interfaces;

/// <summary>
/// Interface for data services that use database repositories.
/// Extracts <see cref="TEntity"/> from database and returns <see cref="TDTO"/>
/// </summary>
public interface IDataService<TEntity, TDTO> : IReadDataService<TEntity, TDTO>, IWriteDataService<TEntity, TDTO>, IDisposable
    where TEntity : class
{
    
}

/// <summary>
/// Interface for data services that use database repositories and do not require SaveChanges.
/// Extracts <see cref="TEntity"/> from database and returns <see cref="TDTO"/>
/// </summary>
public interface IReadDataService<TEntity, TDTO> : IDisposable
    where TEntity : class
{
    TDTO? GetById(int id);
    TDTO[] GetAll();
    TDTO[] GetAll(Expression<Func<TEntity, bool>> expression);
    TDTO[] GetAll(params Expression<Func<TEntity, object>>[] includeProperties);
    Task<TDTO[]> GetAllAsync();
    Task<TDTO[]> GetAllAsync(Expression<Func<TEntity, bool>> expression);
    Task<TDTO[]> GetAllAsync(params Expression<Func<TEntity, object>>[] includeProperties);
    bool Contains(TDTO dto);
    bool Contains(int id);
}

/// <summary>
/// Interface for data services that use database repositories and require SaveChanges.
/// Extracts <see cref="TEntity"/> from database and returns <see cref="TDTO"/>
/// </summary>
public interface IWriteDataService<TEntity, TDTO> : IDisposable
    where TEntity : class
{
    Task Add(TDTO dto);
    Task AddRange(IEnumerable<TDTO> entities);
    Task Remove(TDTO dto);
    Task Remove(int id);
    Task RemoveRange(IEnumerable<TDTO> entities);
    Task Update(TDTO dto);
    Task UpdateRange(IEnumerable<TDTO> entities);
}