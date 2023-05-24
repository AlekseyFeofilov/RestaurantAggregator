namespace RestaurantAggregator.Backend.Common.Exceptions.NotFoundException;

public class DishNotFoundException : RestaurantAggregator.Common.Exceptions.NotFoundException
{
    public DishNotFoundException(Guid id) : base(id)
    {
    }
}