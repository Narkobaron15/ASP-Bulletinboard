using AutoMapper;
using Business_Logic.Interfaces;
using Repository.Interfaces;
using System.Linq.Expressions;

namespace Business_Logic.Services;

// Provides basic read logic for one repository - database entity.
public class BasicDataService<TEntity, TDTO> : IReadDataService<TEntity, TDTO>, IDisposable where TEntity : class
{
    private IGenericUnitOfWork GenericUnitOfWork { get; }
    protected IGenericRepository<TEntity> Repo;
    protected IMapper Mapper { get; }

    public BasicDataService(IGenericUnitOfWork GenericUnitOfWork, IMapper Mapper)
    {
        this.GenericUnitOfWork = GenericUnitOfWork;
        this.Repo = GenericUnitOfWork.Repository<TEntity>();
        this.Mapper = Mapper;
    }

    // Turns an entity item into data transfer object
    protected virtual TDTO ToDTO(TEntity entity)
        => Mapper.Map<TEntity, TDTO>(entity);
    // Turns a data transfer object into entity item
    protected virtual TEntity ToEntity(TDTO dto)
        => Mapper.Map<TDTO, TEntity>(dto);
    // Turns entity items into data transfer objects
    protected virtual TDTO[] ToDTOS(IEnumerable<TEntity> entities)
        => Mapper.Map<IEnumerable<TEntity>, IEnumerable<TDTO>>(entities).ToArray();
    // Turns data transfer objects into entity items
    protected virtual TEntity[] ToEntities(IEnumerable<TDTO> dtos)
        => Mapper.Map<IEnumerable<TDTO>, IEnumerable<TEntity>>(dtos).ToArray();

    public virtual TDTO? GetById(int id)
    {
        var item = Repo.FindById(id);
        return item == null ? default : ToDTO(item);
    }

    public TDTO[] GetAll()
        => ToDTOS(Repo.GetAll());
    public TDTO[] GetAll(Expression<Func<TEntity, bool>> expression)
        => ToDTOS(Repo.GetAll(expression));
    public TDTO[] GetAll(params Expression<Func<TEntity, object>>[] includeProperties)
        => ToDTOS(Repo.GetWithInclude(includeProperties).Result);

    public async Task<TDTO[]> GetAllAsync()
        => ToDTOS(await Repo.GetAllAsync());
    public async Task<TDTO[]> GetAllAsync(Expression<Func<TEntity, bool>> expression)
        => ToDTOS(await Repo.GetAllAsync(expression));
    public async Task<TDTO[]> GetAllAsync(params Expression<Func<TEntity, object>>[] includeProperties)
        => ToDTOS(await Repo.GetWithInclude(includeProperties));

    public bool Contains(TDTO dto)
        => Repo.Contains(ToEntity(dto));
    public bool Contains(int id)
        => this.GetById(id) != null;

    public void Dispose()
	{
        GenericUnitOfWork.Dispose();
        GC.SuppressFinalize(this);
	}
}
