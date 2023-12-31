using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RestaurantAggregator.Auth.Common.Dtos;
using RestaurantAggregator.Auth.Common.IServices;
using RestaurantAggregator.Backend.Common.Configurations;
using RestaurantAggregator.Common.Configurations;
using RestaurantAggregator.Common.Extensions;
using AppConfigurations = RestaurantAggregator.Auth.Common.Configurations.AppConfigurations;

namespace RestaurantAggregator.Auth.BL.Services;

public class JwtService : IJwtService
{
    private readonly IOptions<JwtConfigurations> _configurations;

    public JwtService(IOptions<JwtConfigurations> configurations)
    {
        _configurations = configurations;
    }

    public TokenDto GenerateToken(List<Claim> claims)
    {
        return new TokenDto
        (
            GenerateAccessToken(claims),
            GenerateRefreshToken()
        );
    }

    private string GenerateAccessToken(List<Claim> claims)
    {
        var now = DateTime.UtcNow;

        var jwtToken = new JwtSecurityToken(
            issuer: _configurations.Value.Issuer,
            audience: _configurations.Value.Audience,
            notBefore: now,
            claims: claims,
            expires: now.AddMinutes(_configurations.Value.Lifetime),
            signingCredentials: new SigningCredentials
            (
                _configurations.Value.Key.ToSymmetricSecurityKey(),
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