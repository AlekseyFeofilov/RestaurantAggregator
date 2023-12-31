using RestaurantAggregator.Common.Dtos;

namespace RestaurantAggregator.Common.Extensions;

public static class PagedEnumerableExtension
{
    public static PagedEnumerable<T> GetPagedEnumerable<T>(this IEnumerable<T> enumerable, int totalCount, int pageSize, int page)
    {
        // var totalCount = (page - 1) * pageSize + enumerable.Count();
        
        var pageCount = (int)Math.Ceiling(totalCount * 1.0 / pageSize);
        var enumerableCount = page < pageCount ? pageSize : Math.Max(0, totalCount - pageSize * (page - 1));
        if (enumerableCount == 0) throw new ();

        return new PagedEnumerable<T>(
            enumerable,
            new PageInfo(
                enumerableCount / pageSize,
                pageCount,
                page
            )
        );
    }
}