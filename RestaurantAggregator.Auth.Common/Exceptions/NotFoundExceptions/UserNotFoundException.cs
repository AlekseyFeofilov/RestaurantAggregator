using RestaurantAggregator.Common.Exceptions;

namespace RestaurantAggregator.Auth.Common.Exceptions.NotFoundExceptions;

public class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(Guid id) : base(id)
    {
    }
}