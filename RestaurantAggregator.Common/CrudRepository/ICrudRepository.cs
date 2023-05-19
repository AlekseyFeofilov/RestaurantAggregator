namespace RestaurantAggregator.Common.CrudRepository;

public interface ICrudRepository<T>
{
    public IQueryable<T> FetchAllElements();

    public T FetchDetails(Guid id);

    public Task CreateAsync(T element);

    public Task ModifyAsync(T element);

    public Task DeleteAsync(Guid id);
}