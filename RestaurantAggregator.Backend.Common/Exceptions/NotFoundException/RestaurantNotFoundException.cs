namespace RestaurantAggregator.Backend.Common.Exceptions.NotFoundException;

public class RestaurantNotFoundException : RestaurantAggregator.Common.Exceptions.NotFoundException
{
    public RestaurantNotFoundException(Guid id) : base(id)
    {
    }
}