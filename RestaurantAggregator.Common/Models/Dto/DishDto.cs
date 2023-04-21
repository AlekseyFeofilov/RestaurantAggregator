using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using RestaurantAggregator.Common.Models.Enums;

namespace RestaurantAggregator.Common.Models.Dto;

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

    [Range(1, 10)]
    public double? Rating { get; set; }
 
    public DishCategory Category { get; set; }
    
    public RestaurantDto Restaurant { get; set; }
}