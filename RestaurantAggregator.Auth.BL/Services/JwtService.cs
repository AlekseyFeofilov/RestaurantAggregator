using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using RestaurantAggregator.Auth.Common.IServices;
using RestaurantAggregator.Auth.Common.Models.Dtos;
using RestaurantAggregator.Common.Configurations;

namespace RestaurantAggregator.Auth.BL.Services;

public class JwtService : IJwtService
{
    public TokenDto GenerateToken(List<Claim> claims)
    {
        return new TokenDto
        (
            GenerateAccessToken(claims),
            GenerateRefreshToken()
        );
    }

    public TokenDto RefreshToken(TokenDto tokenDto)
    {
        throw new NotImplementedException();
    }

    private string GenerateAccessToken(List<Claim> claims)
    {
        var now = DateTime.UtcNow;

        var jwtToken = new JwtSecurityToken(
            issuer: JwtConfigurations.Issuer,
            audience: JwtConfigurations.Audience,
            notBefore: now,
            claims: claims,
            expires: now.AddMinutes(JwtConfigurations.Lifetime),
            signingCredentials: new SigningCredentials
            (
                JwtConfigurations.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256
            ));

        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var randomNumberGenerator = RandomNumberGenerator.Create();

        randomNumberGenerator.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}