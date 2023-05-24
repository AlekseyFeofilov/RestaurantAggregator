namespace RestaurantAggregator.Backend.Common.Exceptions.NotFoundException;

public class CookNotfoundException : RestaurantAggregator.Common.Exceptions.NotFoundException
{
    public CookNotfoundException(Guid id) : base(id)
    {
    }
}