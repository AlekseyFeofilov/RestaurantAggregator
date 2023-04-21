using System.ComponentModel.DataAnnotations;
using RestaurantAggregator.Common.Models.Enums;

namespace RestaurantAggregator.Common.Models.Dto;

public class DishModifyDto
{
    [MinLength(1)]
    public string? Name { get; set; }
 
    public string? Description { get; set; }

    [Range(0, double.MaxValue)]
    public double? Price { get; set; }
 
    public string? Image { get; set; }
     
    public bool? Vegetarian { get; set; }

    public DishCategory? Category { get; set; }
    
    public Guid? RestaurantId { get; set; }
}