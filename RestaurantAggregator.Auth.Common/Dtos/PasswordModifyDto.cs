using System.ComponentModel.DataAnnotations;

namespace RestaurantAggregator.Auth.Common.Dtos;

public class PasswordModifyDto
{
    [Required]
    public string CurrentPassword { get; set; }
    
    [Required]
    public string NewPassword { get; set; }
}