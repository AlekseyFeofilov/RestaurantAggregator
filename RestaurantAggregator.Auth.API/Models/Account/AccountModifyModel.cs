using System.ComponentModel.DataAnnotations;
using RestaurantAggregator.Common.Attributes.ValidationAttributes;
using RestaurantAggregator.Common.Dtos.Enums;

namespace RestaurantAggregator.Auth.API.Models.Account;

public class AccountModifyModel
{
    [MinLength(1)]
    public string? FullName { get; set; }
    
    [DateRange(laterThanTodayBy: 0, isNullable: true)]
    public DateTime? BirthDate { get; set; }
    
    public Gender? Gender { get; set; }
    
    [Phone]
    public string? PhoneNumber { get; set; }
    
    public string? Address { get; set; }
}