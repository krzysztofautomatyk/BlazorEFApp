using System.Linq.Expressions;

namespace BlazorEFApp.Domain.Repositories;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    void Update(T entity);   
    Task DeleteAsync(int id);
    Task<IEnumerable<T>> GetPagedAsync(int page, int pageSize);
    Task<IEnumerable<T>> GetFilteredAsync(Expression<Func<T, bool>> predicate, int page = 1, int pageSize = 20);
    Task<IEnumerable<T>> GetAllWithIncludeAsync(params Expression<Func<T, object>>[] includeProperties);

}
