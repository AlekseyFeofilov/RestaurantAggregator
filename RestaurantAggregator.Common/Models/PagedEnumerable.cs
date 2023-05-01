namespace RestaurantAggregator.Common.Models;

public class PagedEnumerable<T>
{
    public IEnumerable<T> Values { get; set; }
    
    public PageInfoModel Pagination { get; set; }

    public PagedEnumerable(IEnumerable<T> values, PageInfoModel pagination)
    {
        Values = values;
        Pagination = pagination;
    }
}