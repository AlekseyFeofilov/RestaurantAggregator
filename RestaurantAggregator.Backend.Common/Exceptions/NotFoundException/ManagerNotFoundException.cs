namespace RestaurantAggregator.Backend.Common.Exceptions.NotFoundException;

public class ManagerNotFoundException : RestaurantAggregator.Common.Exceptions.NotFoundException
{
    public ManagerNotFoundException(Guid id) : base(id)
    {
    }
}