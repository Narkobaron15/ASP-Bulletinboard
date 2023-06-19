using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository.Implementations;

public class EFUnitOfWork<TContext> : IGenericUnitOfWork, IDisposable
    where TContext : DbContext
{
    protected TContext Context { get; }

    protected Dictionary<Type, object> Repositories { get; set; }

    public EFUnitOfWork(TContext Context)
    {
        this.Context = Context;
        Repositories = new();
    }

    public void SaveChanges() => Context.SaveChanges();
    public async Task SaveAsync() => await Context.SaveChangesAsync();

    public IGenericRepository<T> Repository<T>() where T : class
    {
        if (Repositories.TryGetValue(typeof(T), out object? value))
            return (IGenericRepository<T>)value;

        // else
        IGenericRepository<T> repo = new EFRepository<T>(Context);
        Repositories.Add(typeof(T), repo);

        return repo;
    }

    public void Dispose()
    {
        Context.Dispose();
        GC.SuppressFinalize(this);
    }
}
