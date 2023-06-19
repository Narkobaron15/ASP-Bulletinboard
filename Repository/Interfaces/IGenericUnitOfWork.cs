namespace Repository.Interfaces;

public interface IGenericUnitOfWork : IDisposable 
{
    void SaveChanges();
    Task SaveAsync();

    IGenericRepository<T> Repository<T>() where T : class;
}
