namespace RestaurantAggregator.Auth.Common.Exceptions.NotFoundExceptions;

public class ManagerNotFoundException : RestaurantAggregator.Common.Exceptions.NotFoundException
{
    public ManagerNotFoundException(Guid id) : base(id)
    {
    }
}