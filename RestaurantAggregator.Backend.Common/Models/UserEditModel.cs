using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using RestaurantAggregator.Auth.Common.Models.Enums;

namespace RestaurantAggregator.Common.Models;

public class UserEditModel
{
    [MinLength(1)]
    [Required]
    [JsonPropertyName("fullName")]
    public string FullName { get; }
    [JsonPropertyName("birthDate")]
    public DateTime? BirthDate { get; }
    [JsonPropertyName("gender")]
    [Required]
    public Gender Gender { get; }
    [JsonPropertyName("address")]
    public string? Address { get; }
    [Phone]
    [JsonPropertyName("phoneNumber")]
    public string? PhoneNumber { get; }

    public UserEditModel(string fullName, DateTime? birthDate, Gender gender, string? address, string? phoneNumber)
    {
        FullName = fullName;
        BirthDate = birthDate;
        Gender = gender;
        Address = address;
        PhoneNumber = phoneNumber;
    }
}