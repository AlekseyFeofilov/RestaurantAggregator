using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestaurantAggregator.Common.Exceptions;
using RestaurantAggregator.Common.Models;

namespace RestaurantAggregator.Common.CrudRepository;

public abstract class CrudRepository<T> : ICrudRepository<T> where T : EntityWithId
{
    private readonly DbContext _context;

    private readonly DbSet<T> _dbSet;

    protected CrudRepository(DbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public IQueryable<T> FetchAllElements()
    {
        return _dbSet;
    }

    public T FetchDetails(Guid id)
    {
        var entity = _dbSet.SingleOrDefault(x => x.Id == id);

        if (entity == null)
        {
            throw new NotFoundException();
        }

        return entity;
    }

    public async Task CreateAsync(T element)
    {
        await _dbSet.AddAsync(element);
        await SaveChangesAsync();
    }

    public abstract Task ModifyAsync(T element);

    public async Task DeleteAsync(Guid id)
    {
        var entity = FetchDetails(id);
        _dbSet.Remove(entity);
        await SaveChangesAsync();
    }

    protected Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}