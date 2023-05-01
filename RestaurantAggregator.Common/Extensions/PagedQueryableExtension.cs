namespace RestaurantAggregator.Common.Extensions;

public static class PagedQueryableExtension
{
    public static IQueryable GetPagedQueryable<T>(this IQueryable<T> queryable, int skip, int take)
    {
        take = RecalculateTakeCount(queryable.Count(), skip, take);
        
        return queryable
            .Skip(skip)
            .Take(take);
    }

    private static int RecalculateTakeCount(int dishCount, int skip, int take)
    {
        if (dishCount < skip + take) //todo maybe кидать 404, если dishCount <= skip
        {
            return Math.Max(0, dishCount - skip);
        }

        return take;
    }
}