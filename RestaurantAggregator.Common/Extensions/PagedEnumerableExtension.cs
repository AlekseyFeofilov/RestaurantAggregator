using RestaurantAggregator.Common.Models;

namespace RestaurantAggregator.Common.Extensions;

public static class PagedEnumerableExtension
{
    public static PagedEnumerable<T> PageEnumerable<T>(this IEnumerable<T> enumerable, int pageSize, int page)
    {
        var totalCount = enumerable.Count();
        
        var pageCount = (int)Math.Ceiling(totalCount * 1.0 / pageSize);
        var dishCount = page < pageCount ? pageSize : Math.Max(0, totalCount - pageSize * (page - 1));
        if (dishCount == 0) throw new (); //crutch: нужна свой тип ошибки

        return new PagedEnumerable<T>(
            enumerable,
            new PageInfoModel(
                dishCount,
                pageCount,
                page
            )
        );
    }
}