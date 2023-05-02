using System.ComponentModel.DataAnnotations;
using RestaurantAggregator.Common.Models.Dto.Restaurant;
using RestaurantAggregator.Common.Models.Enums;

namespace RestaurantAggregator.Common.Models.Dto.Dish;

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