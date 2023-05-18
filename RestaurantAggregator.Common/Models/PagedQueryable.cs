namespace RestaurantAggregator.Common.Models;

public class PagedQueryable<T>
{
    public IQueryable<T> Items { get; set; }
    
    public PageInfoModel Pagination { get; set; }

    public PagedQueryable(IQueryable<T> items, PageInfoModel pagination)
    {
        Items = items;
        Pagination = pagination;
    }
}