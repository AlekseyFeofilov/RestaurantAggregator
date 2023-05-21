using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestaurantAggregator.Common.Exceptions;
using RestaurantAggregator.Common.Models;

namespace RestaurantAggregator.Common.CrudRepository;

public abstract class CrudRepository<T> : ICrudRepository<T> where T : class, IClassWithId
{
    private readonly DbContext _context;

    protected readonly DbSet<T> DbSet;

    protected CrudRepository(DbContext context)
    {
        _context = context;
        DbSet = context.Set<T>();
    }

    public IQueryable<T> FetchAllElements()
    {
        return DbSet;
    }

    public T FetchDetails(Guid id)
    {
        var entity = PrepareToFetchDetails().SingleOrDefault(x => x.Id == id);

        if (entity == null)
        {
            throw new NotFoundException();
        }

        return entity;
    }

    public async Task CreateAsync(T element)
    {
        await DbSet.AddAsync(element);
        await SaveChangesAsync();
    }

    public abstract Task ModifyAsync(T element);

    public async Task DeleteAsync(Guid id)
    {
        var entity = FetchDetails(id);
        DbSet.Remove(entity);
        await SaveChangesAsync();
    }

    protected Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }

    protected virtual IQueryable<T> PrepareToFetchAll()
    {
        return DbSet;
    }


    protected virtual IQueryable<T> PrepareToFetchDetails()
    {
        return DbSet;
    }
}