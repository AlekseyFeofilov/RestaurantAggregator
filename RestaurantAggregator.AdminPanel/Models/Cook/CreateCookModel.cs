using System.ComponentModel.DataAnnotations;

namespace RestaurantAggregator.AdminPanel.Models.Cook;

public class CreateCookModel
{
    [Required] 
    public Guid Id { get; set; }

    [Required]
    public Guid RestaurantId { get; set; }
}