using System.ComponentModel.DataAnnotations;

namespace RestaurantAggregator.AdminPanel.Models.Restaurant;

public class CreateRestaurantModel
{
    [MinLength(1)]
    [Required]
    public string Name { get; set; }
}