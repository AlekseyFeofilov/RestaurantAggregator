using System.ComponentModel.DataAnnotations;

namespace RestaurantAggregator.AdminPanel.Models.Courier;

public class CreateCourierModel
{
    [Required] 
    public Guid Id { get; set; }

    [Required]
    public Guid RestaurantId { get; set; }
}