namespace RestaurantAggregator.Backend.Common.Exceptions.NotFoundException;

public class MenuNotFoundException : RestaurantAggregator.Common.Exceptions.NotFoundException
{
    public MenuNotFoundException(Guid id) : base(id)
    {
    }
}