namespace RestaurantAggregator.Auth.Common.Exceptions.NotFoundExceptions;

public class CookNotFoundException : RestaurantAggregator.Common.Exceptions.NotFoundException
{
    public CookNotFoundException(Guid id) : base(id)
    {
    }
}