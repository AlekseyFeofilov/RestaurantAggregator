using System.ComponentModel.DataAnnotations;
using RestaurantAggregator.Common.Dtos.Enums;

namespace RestaurantAggregator.Auth.API.Models.Account;

public class AccountModel
{
    [Required]
    public string FullName { get; set; }
    
    public DateTime? BirthDate { get; set; }
    
    public Gender? Gender { get; set; }
    
    [Phone]
    public string? PhoneNumber { get; set; }
    
    public string? Address { get; set; }
    
    [EmailAddress, Required]
    public string Email { get; set; }
    
    public string Roles { get; set; }
}