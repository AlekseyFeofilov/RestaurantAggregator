using RestaurantAggregator.Common.Models;

namespace RestaurantAggregator.Common.Extensions;

public static class PagedQueryableExtension
{
    public static PagedQueryable<T> GetPagedQueryable<T>(this IQueryable<T> queryable, int page, int pageSize)
    {
        var skip = (page - 1) * pageSize;
        var take = CalculateTakeCount(queryable.Count(), skip, pageSize);

        return new PagedQueryable<T>(
            queryable.Skip(skip).Take(take),
            new PageInfoModel((queryable.Count() + pageSize - 1) / pageSize, take, page)
        );
    }

    private static int CalculateTakeCount(int count, int skip, int take)
    {
        return count < skip + take
            ? //todo maybe кидать 404, если dishCount <= skip
            Math.Max(0, count - skip)
            : take;
    }
}