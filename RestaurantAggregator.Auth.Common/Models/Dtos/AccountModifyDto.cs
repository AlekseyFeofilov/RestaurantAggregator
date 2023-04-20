using System.ComponentModel.DataAnnotations;
using RestaurantAggregator.Auth.Common.Models.Enums;

namespace RestaurantAggregator.Auth.Common.Models.Dtos;

public class AccountModifyDto
{
    [MinLength(1)]
    public string? FullName { get; set; }
    
    public DateTime? BirthDate { get; set; }
    
    public Gender? Gender { get; set; }
    
    [Phone]
    public string? PhoneNumber { get; set; }
    
    public string? Address { get; set; }
}