namespace RestaurantAggregator.Common.Dtos;

public class PagedEnumerable<T>
{
    public IEnumerable<T> Items { get; set; }
    
    public PageInfo Pagination { get; set; }

    public PagedEnumerable(IEnumerable<T> items, PageInfo pagination)
    {
        Items = items;
        Pagination = pagination;
    }
}