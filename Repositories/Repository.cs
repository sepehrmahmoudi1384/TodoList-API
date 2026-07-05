using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace TodoList.API.Repositories;

public class Repository<T>(DbContext dbContext)
    : IRepository<T> where T : class
{
    protected readonly DbContext _dbContext = dbContext;

    public async Task AddAsync(T entity)
    {
        await _dbContext.Set<T>()
            .AddAsync(entity);
    }

    public async Task DeleteAsync(T entity)
    {
        _dbContext.Set<T>()
            .Remove(entity);
    }

    public async Task<IEnumerable<T>> FindAsync(
        Expression<Func<T, bool>> predicate
    )
    {
        return await _dbContext.Set<T>()
            .Where(predicate).ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbContext.Set<T>()
            .ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbContext.Set<T>()
            .FindAsync(id);
    }

    public async Task UpdateAsync(T entity)
    {
        _dbContext.Set<T>()
            .Update(entity);
    }
}