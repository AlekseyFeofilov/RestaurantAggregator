namespace RestaurantAggregator.Backend.Common.Exceptions.NotFoundException;

public class OrderNotFoundException : RestaurantAggregator.Common.Exceptions.NotFoundException
{
    public OrderNotFoundException(Guid id) : base(id)
    {
    }
}