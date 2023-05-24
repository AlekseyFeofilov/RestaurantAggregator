using System.Security.Claims;
using RestaurantAggregator.Auth.Common.Dtos;

namespace RestaurantAggregator.Auth.Common.IServices;

public interface IJwtService
{
    TokenDto GenerateToken(List<Claim> claimsIdentity);
}