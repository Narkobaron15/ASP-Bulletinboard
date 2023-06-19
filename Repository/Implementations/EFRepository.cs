using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System.Linq.Expressions;

namespace Repository.Implementations;

public class EFRepository<TEntity> : IGenericRepository<TEntity> 
    where TEntity : class
{
    private readonly DbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public EFRepository(DbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }


    // CREATE
    public void Add(TEntity entity)
    {
        _dbSet.Add(entity);
        _context.SaveChanges();
    }
    public void AddRange(IEnumerable<TEntity> entities)
    {
        _dbSet.AddRange(entities);
        _context.SaveChanges();
    }
    public async Task AddAsync(TEntity item)
    {
        await _dbSet.AddAsync(item);
        await _context.SaveChangesAsync();
    }
    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await _dbSet.AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }


    // READ

    // SINGLE ITEM READ
    public TEntity? FindById(params object?[]? id) => _dbSet.Find(id);
    public async Task<TEntity?> FindByIdAsync(params object?[]? id) => await _dbSet.FindAsync(id);


    // MULTI ITEM READ
    public IEnumerable<TEntity> GetAll() => _dbSet.ToList();
    public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> _pred) => _dbSet.Where(_pred).ToList();
    public async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbSet.ToListAsync();
    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> _pred)
        => await _dbSet.Where(_pred).ToListAsync();
    public async Task<IEnumerable<TEntity>> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties)
        => await Include(includeProperties).ToListAsync();
    public async Task<IEnumerable<TEntity>> GetWithInclude(
        Func<TEntity, bool> predicate,
        params Expression<Func<TEntity, object>>[] includeProperties
        )
    {
        var query = Include(includeProperties);
        return (await query.ToListAsync()).Where(predicate);
    }

    // READ MISCELLANEOUS
    public bool Contains(TEntity entity) => _dbSet.Contains(entity);
    public async Task<bool> ContainsAsync(TEntity entity) => await _dbSet.ContainsAsync(entity);
    private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = _dbSet;
        return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
    }


    // UPDATE
    public void Update(TEntity entity)
    {
        _context.Update(entity);
        _context.SaveChanges();
    }
    public void UpdateRange(IEnumerable<TEntity> entities)
    {
        _context.UpdateRange(entities);
        _context.SaveChanges();
    }
    public async Task UpdateAsync(TEntity item)
    {
        _context.Update(item);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
    {
        _context.UpdateRange(entities);
        await _context.SaveChangesAsync();
    }


    // REMOVE
    public void Remove(TEntity entity)
    {
        _dbSet.Remove(entity);
        _context.SaveChanges();
    }

    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        _context.RemoveRange(entities);
        _context.SaveChanges();
    }
    public async Task RemoveAsync(TEntity item)
    {
        _dbSet.Remove(item);
        await _context.SaveChangesAsync();
    }
    public async Task RemoveRangeAsync(IEnumerable<TEntity> entities)
    {
        _dbSet.RemoveRange(entities);
        await _context.SaveChangesAsync();
    }
}
