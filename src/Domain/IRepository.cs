namespace Domain;

public interface IRepository
{
    IQueryable<T> Query<T>() where T : class;
    Task AddAsync<T>(T entity) where T : class;
    void Update<T>(T entity) where T : class;
    void Delete<T>(T entity) where T : class;
    Task<bool> CommitAsync();
}