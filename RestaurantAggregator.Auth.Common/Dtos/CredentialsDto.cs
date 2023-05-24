using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RestaurantAggregator.Auth.Common.Dtos;

public class CredentialsDto
{
    [JsonPropertyName("email")]
    [EmailAddress, Required]
    public string Email { get; set; }
    
    [JsonPropertyName("password")]
    [Required]
    public string Password { get; set; }
}