using System.ComponentModel.DataAnnotations;
using RestaurantAggregator.Auth.Common.Models.Enums;

namespace RestaurantAggregator.Auth.Common.Models.Dtos;

public class AccountCreateDto
{
    [MinLength(1), Required]
    public string FullName { get; set; }
    
    public DateTime? BirthDate { get; set; }
    
    public Gender? Gender { get; set; }
    
    [Phone]
    public string? PhoneNumber { get; set; } //todo запретить делать заказ при неуказанном номере и адресе
    
    public string? Address { get; set; }
    
    [EmailAddress, Required]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
}