using System.ComponentModel.DataAnnotations;

namespace RestaurantAggregator.AdminPanel.Models.Manager;

public class CreateManagerModel
{
    [MinLength(1)]
    [Required]
    public string Name { get; set; }
    
    [Required]
    public Guid restaurantId { get; set; }
}