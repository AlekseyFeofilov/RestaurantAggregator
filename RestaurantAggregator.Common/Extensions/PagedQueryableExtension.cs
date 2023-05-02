namespace RestaurantAggregator.Common.Extensions;

public static class PagedQueryableExtension
{
    public static IQueryable<T> GetPagedQueryable<T>(this IQueryable<T> queryable, int skip, int take)
    {
        take = RecalculateTakeCount(queryable.Count(), skip, take);
        
        return queryable
            .Skip(skip)
            .Take(take);
    }

    private static int RecalculateTakeCount(int count, int skip, int take)
    {
        return count < skip + take ? //todo maybe кидать 404, если dishCount <= skip
            Math.Max(0, count - skip) : take;
    }
}