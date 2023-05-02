using System.ComponentModel.DataAnnotations;
using RestaurantAggregator.Common.Models.Enums;

namespace RestaurantAggregator.API.Models.Dish;

public class DishCreateModel
{
    [MinLength(1), Required]
    public string Name { get; set; }
 
    public string? Description { get; set; }

    [Range(0, double.MaxValue), Required]
    public double Price { get; set; }
 
    public string? Image { get; set; }
     
    public bool Vegetarian { get; set; }

    public DishCategory Category { get; set; }
    
    public Guid RestaurantId { get; set; }
}