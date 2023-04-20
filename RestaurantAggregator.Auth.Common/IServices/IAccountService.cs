using System.Security.Claims;
using RestaurantAggregator.Auth.Common.Models.Dtos;

namespace RestaurantAggregator.Auth.Common.IServices;

public interface IAccountService
{
    Task<AccountDto> FetchProfileInfo(ClaimsPrincipal claimsPrincipal);
    Task<AccountDto> ModifyProfileInfo(ClaimsPrincipal claimsPrincipal, AccountModifyDto accountModifyDto);
    Task ChangePassword(ClaimsPrincipal claimsPrincipal, PasswordModifyDto passwordModifyDto);
}