using System.ComponentModel.DataAnnotations;

namespace RestaurantAggregator.AdminPanel.Models;

public class CreateRestaurantModel
{
    [MinLength(1)]
    [Required]
    public string Name { get; set; }
}