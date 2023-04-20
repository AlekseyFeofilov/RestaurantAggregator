using System.Security.Claims;
using RestaurantAggregator.Auth.Common.Models.Dtos;

namespace RestaurantAggregator.Auth.Common.IServices;

public interface IAuthService
{
    Task<TokenDto> LogInAsync(CredentialsDto credentialsDto);
    
    Task<TokenDto> SignUpAsync(AccountCreateDto accountCreateDto);
    
    Task<TokenDto> RefreshTokenAsync(TokenDto tokenDto);
    
    Task LogOutAsync(ClaimsPrincipal claimsPrincipal);
}