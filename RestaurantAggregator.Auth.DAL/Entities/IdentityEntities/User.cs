using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using RestaurantAggregator.Auth.Common.Models.Enums;
using RestaurantAggregator.Auth.DAL.Entities.Users;

namespace RestaurantAggregator.Auth.DAL.Entities.IdentityEntities;

public class User : IdentityUser<Guid>
{
    [MinLength(1), Required]
    public string FullName { get; set; }
    
    public DateTime? BirthDate { get; set; }
    
    [Required]
    public Gender Gender { get; set; }
    
    public string? RefreshToken { get; set; }
}