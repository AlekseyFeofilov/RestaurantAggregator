namespace RestaurantAggregator.Auth.Common.Exceptions.NotFoundExceptions;

public class CustomerNotFoundException : RestaurantAggregator.Common.Exceptions.NotFoundException
{
    public CustomerNotFoundException(Guid id) : base(id)
    {
    }
}