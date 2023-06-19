using AutoMapper;
using Business_Logic.Interfaces;
using Repository.Interfaces;

namespace Business_Logic.Services;

// Provides read-write logic within one repository - database entity.
public class DataService<TEntity, TDTO> : BasicDataService<TEntity, TDTO>, IDataService<TEntity, TDTO>, IDisposable
	where TEntity : class
{
    public DataService(IGenericUnitOfWork genericUnitOfWork, IMapper mapper)
	    : base(genericUnitOfWork, mapper) { }

	public virtual async Task Add(TDTO entity)
    {
        await Repo.AddAsync(ToEntity(entity));
    }
    public virtual async Task AddRange(IEnumerable<TDTO> entities)
    {
        await Repo.AddRangeAsync(ToEntities(entities));
    }

    public virtual async Task Update(TDTO entity)
	{
		await Repo.UpdateAsync(ToEntity(entity));
    }
    public virtual async Task UpdateRange(IEnumerable<TDTO> entities)
    {
        await Repo.UpdateRangeAsync(ToEntities(entities));
    }

    public virtual async Task Remove(TDTO entity)
	{
        await Repo.RemoveAsync(ToEntity(entity));
    }
	public virtual async Task Remove(int id)
	{
        await Repo.RemoveAsync(Repo.FindById(id)!);
    }
    public virtual async Task RemoveRange(IEnumerable<TDTO> entities)
    {
        await Repo.RemoveRangeAsync(ToEntities(entities));
    }
}
