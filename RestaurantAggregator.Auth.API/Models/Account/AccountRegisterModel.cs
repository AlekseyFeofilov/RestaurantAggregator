using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using RestaurantAggregator.Common.Dtos.Enums;

namespace RestaurantAggregator.Auth.API.Models.Account;

public class AccountRegisterModel
{
    [MinLength(1), Required]
    public string FullName { get; set; }
    
    public DateTime? BirthDate { get; set; }
    
    public Gender? Gender { get; set; }
    
    [Phone, Required]
    public string PhoneNumber { get; set; }
    
    [Required]
    public string Address { get; set; }
    
    [EmailAddress, Required]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
}