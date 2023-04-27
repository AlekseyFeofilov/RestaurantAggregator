using RestaurantAggregator.Common.Models.Enums;

namespace RestaurantAggregator.DAL.Models;

public class FetchDishOptions
{
    public Guid RestaurantId { get; set; }
    
    public Guid? MenuId { get; set; }
    
    public DishCategory[]? Categories { get; set; }
    
    public bool Vegetarian { get; set; }
    
    public DishSorting? Sorting { get; set; }

    public int Skip { get; set; }
    
    public int Take { get; set; }
}