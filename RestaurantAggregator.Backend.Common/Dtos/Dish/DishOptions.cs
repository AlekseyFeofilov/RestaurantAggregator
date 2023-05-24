using RestaurantAggregator.Common.Dtos.Enums;

namespace RestaurantAggregator.Backend.Common.Dtos.Dish;


public class DishOptions
{
    public Guid RestaurantId { get; set; }
    
    public Guid? MenuId { get; set; }
    
    public DishCategory[]? Categories { get; set; }
    
    public bool Vegetarian { get; set; }
    
    public DishSorting? Sorting { get; set; }
    
    public int Page { get; set; }

    public DishOptions(Guid restaurantId, Guid? menuId, DishCategory[]? categories, bool vegetarian, DishSorting? sorting, int page)
    {
        RestaurantId = restaurantId;
        MenuId = menuId;
        Categories = categories;
        Vegetarian = vegetarian;
        Sorting = sorting;
        Page = page;
    }

    public DishOptions()
    {
    }
}