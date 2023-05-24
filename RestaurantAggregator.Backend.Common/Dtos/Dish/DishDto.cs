using System.ComponentModel.DataAnnotations;
using RestaurantAggregator.Backend.Common.Dtos.Restaurant;
using RestaurantAggregator.Common.Dtos.Enums;

namespace RestaurantAggregator.Backend.Common.Dtos.Dish;

public class DishDto
{ 
    public Guid Id { get; set; }

    [MinLength(1), Required]
    public string Name { get; set; }
 
    public string? Description { get; set; }

    [Range(0, double.MaxValue), Required]
    public double Price { get; set; }
 
    public string? Image { get; set; }
     
    public bool Vegetarian { get; set; }
    
    public int ReviewsAverageScore { get; set; }
 
    public DishCategory Category { get; set; }
    
    public RestaurantDto Restaurant { get; set; }
}