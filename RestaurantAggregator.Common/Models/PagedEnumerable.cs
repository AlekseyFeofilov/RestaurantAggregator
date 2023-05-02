namespace RestaurantAggregator.Common.Models;

public class PagedEnumerable<T>
{
    public IEnumerable<T> Items { get; set; }
    
    public PageInfoModel Pagination { get; set; }

    public PagedEnumerable(IEnumerable<T> items, PageInfoModel pagination)
    {
        Items = items;
        Pagination = pagination;
    }
}