namespace RestaurantAggregator.Common.Dtos;

public class PagedQueryable<T>
{
    public IQueryable<T> Items { get; set; }
    
    public PageInfo Pagination { get; set; }

    public PagedQueryable(IQueryable<T> items, PageInfo pagination)
    {
        Items = items;
        Pagination = pagination;
    }
}