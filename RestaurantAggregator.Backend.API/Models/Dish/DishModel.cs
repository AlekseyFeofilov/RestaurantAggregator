using System.ComponentModel.DataAnnotations;
using RestaurantAggregator.API.Models.Restaurant;
using RestaurantAggregator.Common.Models.Enums;

namespace RestaurantAggregator.API.Models.Dish;

public class DishModel // todo maybe сделать shortModel. За одно проверить гибкость моделей, когда они разделены по слоям
{
    public Guid Id { get; set; }

    [MinLength(1), Required]
    public string Name { get; set; }
 
    public string? Description { get; set; }

    [Range(0, double.MaxValue), Required]
    public double Price { get; set; }
 
    public string? Image { get; set; }
     
    public bool Vegetarian { get; set; }

    [Range(0, 10)]
    public int ReviewsAverageScore { get; set; }
 
    public DishCategory Category { get; set; }
    
    public RestaurantModel Restaurant { get; set; }
}