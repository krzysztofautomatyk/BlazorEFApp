using BlazorEFApp.Domain.Repositories;
using BlazorEFApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    //public async Task<IEnumerable<T>> GetAllAsync() =>
    //    await _dbSet.AsNoTracking().ToListAsync();


    public async Task<IEnumerable<T>> GetAllAsync() =>
        await _dbSet.AsNoTracking().ToListAsync();

    public async Task<T?> GetByIdAsync(int id) =>
        await _dbSet.FindAsync(id);

    public async Task AddAsync(T entity) =>
        await _dbSet.AddAsync(entity);


    public void Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
    }


    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null) _dbSet.Remove(entity);
    }

    public async Task<IEnumerable<T>> GetPagedAsync(int page, int pageSize) =>
        await _dbSet.AsNoTracking()
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

    public async Task<IEnumerable<T>> GetFilteredAsync(
        Expression<Func<T, bool>> predicate,
        int page = 1,
        int pageSize = 20) =>
        await _dbSet.AsNoTracking()
                    .Where(predicate)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

    public async Task<IEnumerable<T>> GetAllWithIncludeAsync(params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = _dbSet;
        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }
        return await query.AsNoTracking().ToListAsync();
    }

}
