using System.ComponentModel.DataAnnotations;

namespace RestaurantAggregator.Auth.Common.Models.Dtos;

public class TokenDto
{
    [Required]
    public string AccessToken { get; set; }
    
    [Required]
    public string RefreshToken { get; set; }

    public TokenDto(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }

    public TokenDto()
    {
    }
}