namespace RestaurantAggregator.Backend.Common.Exceptions.ForbiddenException;

public class DishInCartNotAvailableException : RestaurantAggregator.Common.Exceptions.ForbiddenException
{
    public Guid dishId { get; set; }

    public DishInCartNotAvailableException(Guid dishId)
    {
        this.dishId = dishId;
    }
}