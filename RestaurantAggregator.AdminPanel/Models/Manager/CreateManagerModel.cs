using System.ComponentModel.DataAnnotations;

namespace RestaurantAggregator.AdminPanel.Models.Manager;

public class CreateManagerModel
{
    [Required] 
    public Guid Id { get; set; }

    [Required]
    public Guid RestaurantId { get; set; }
}