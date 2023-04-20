using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RestaurantAggregator.Auth.BL.Extensions;
using RestaurantAggregator.Auth.Common.Exceptions;
using RestaurantAggregator.Auth.Common.IServices;
using RestaurantAggregator.Auth.Common.Models.Dtos;
using RestaurantAggregator.Auth.Common.Models.Enums;
using RestaurantAggregator.Auth.DAL.DbContexts;
using RestaurantAggregator.Auth.DAL.Entities.IdentityEntities;

namespace RestaurantAggregator.Auth.BL.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<User> _userManager;

    private readonly IMapper _mapper;

    private readonly ApplicationDbContext _context;

    public AccountService(UserManager<User> userManager, IMapper mapper, ApplicationDbContext context)
    {
        _userManager = userManager;
        _mapper = mapper;
        _context = context;
    }

    public async Task<AccountDto> FetchProfileInfo(ClaimsPrincipal claimsPrincipal)
    {
        var user = await _userManager.FindByIdAsync(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier));
        var accountDto = _mapper.Map<AccountDto>(user);
        var roles = await _userManager.GetRolesAsync(user);
        accountDto.Roles = string.Join(", ", roles);
        return accountDto;
    }

    public async Task<AccountDto> ModifyProfileInfo(ClaimsPrincipal claimsPrincipal, AccountModifyDto accountModifyDto)
    {
        var user = await _userManager.FindByIdAsync(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier));
        
        if (accountModifyDto.Gender != null) user.Gender = accountModifyDto.Gender.Value;
        if (accountModifyDto.BirthDate != null) user.BirthDate = accountModifyDto.BirthDate.Value;
        if (accountModifyDto.PhoneNumber != null) user.PhoneNumber = accountModifyDto.PhoneNumber;
        if (accountModifyDto.FullName != null) user.FullName = accountModifyDto.FullName;

        var validators = _userManager.UserValidators;

        foreach (var validator in validators)
        {
            var result = await validator.ValidateAsync(_userManager, user);
            
            if (!result.Succeeded)
            {
                throw new InvalidUserException(result.ErrorsToString());
            }
        }

        await _userManager.UpdateAsync(user);
        
        if (accountModifyDto.Address != null && await _userManager.IsInRoleAsync(user, RoleType.Customer.ToString()))
        {
            var customer = _context.Customers.SingleOrDefault(it => it.User.Id == user.Id);

            if (customer == null)
            {
                throw new CustomerNotFoundException();
            }

            customer.Adress = accountModifyDto.Address;
        }
        
        var accountDto = _mapper.Map<AccountDto>(user);
        if (accountModifyDto.Address != null) accountDto.Address = accountModifyDto.Address;
        var roles = await _userManager.GetRolesAsync(user);
        accountDto.Roles = string.Join(", ", roles);
        
        return accountDto;
    }

    public async Task ChangePassword(ClaimsPrincipal claimsPrincipal, PasswordModifyDto passwordModifyDto)
    {
        var user = await _userManager.FindByIdAsync(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier));
        var changePasswordResult = await _userManager.ChangePasswordAsync(user, passwordModifyDto.CurrentPassword, passwordModifyDto.NewPassword);

        if (!changePasswordResult.Succeeded)
        {
            throw new InvalidUserException(changePasswordResult.ErrorsToString()); //crutch мне было лень писать новую ошибку
        }
    }
}