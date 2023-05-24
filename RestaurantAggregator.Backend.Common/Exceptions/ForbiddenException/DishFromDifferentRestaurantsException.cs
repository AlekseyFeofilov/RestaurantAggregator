namespace RestaurantAggregator.Backend.Common.Exceptions.ForbiddenException;

public class DishFromDifferentRestaurantsException : RestaurantAggregator.Common.Exceptions.ForbiddenException
{
    public Guid FirstDishId { get; set; }
    
    public Guid SecondDishId { get; set; }

    public DishFromDifferentRestaurantsException(Guid firstDishId, Guid secondDishId)
    {
        FirstDishId = firstDishId;
        SecondDishId = secondDishId;
    }
}