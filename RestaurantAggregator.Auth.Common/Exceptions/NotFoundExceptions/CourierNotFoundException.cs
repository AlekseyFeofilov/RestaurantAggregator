namespace RestaurantAggregator.Auth.Common.Exceptions.NotFoundExceptions;

public class CourierNotFoundException : RestaurantAggregator.Common.Exceptions.NotFoundException
{
    public CourierNotFoundException(Guid id) : base(id)
    {
    }
}