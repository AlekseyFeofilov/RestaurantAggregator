namespace RestaurantAggregator.Backend.Common.Exceptions.NotFoundException;

public class CourierNotFoundException : RestaurantAggregator.Common.Exceptions.NotFoundException
{
    public CourierNotFoundException(Guid id) : base(id)
    {
    }
}