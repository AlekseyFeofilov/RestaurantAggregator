using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RestaurantAggregator.Auth.BL.Extensions;
using RestaurantAggregator.Auth.Common.Exceptions;
using RestaurantAggregator.Auth.Common.IServices;
using RestaurantAggregator.Auth.Common.Models.Dtos;
using RestaurantAggregator.Auth.Common.Models.Enums;
using RestaurantAggregator.Auth.DAL.DbContexts;
using RestaurantAggregator.Auth.DAL.Entities.IdentityEntities;
using RestaurantAggregator.Auth.DAL.Entities.Users;
using RestaurantAggregator.Backend.Common.Configurations;

namespace RestaurantAggregator.Auth.BL.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;

    private readonly RoleManager<Role> _roleManager;

    private readonly IMapper _mapper;

    private readonly IJwtService _jwtService;

    private readonly ApplicationDbContext _context;

    public AuthService(UserManager<User> userManager, IMapper mapper, IJwtService jwtService, RoleManager<Role> roleManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _mapper = mapper;
        _jwtService = jwtService;
        _roleManager = roleManager;
        _context = context;
    }

    public async Task<TokenDto> LogInAsync(CredentialsDto credentialsDto)
    {
        var user = await FindUserByCredentialsAsync(credentialsDto);
        var claims = await GenerateUserClaimsAsync(user);
        var tokenDto = _jwtService.GenerateToken(claims);
        user.RefreshToken = tokenDto.RefreshToken;
        await _userManager.UpdateAsync(user);
        return tokenDto;
    }
    
    public async Task<TokenDto> SignUpAsync(AccountCreateDto accountCreateDto)
    {
        var validationResult = await ValidateUserAsync(accountCreateDto); // bug: если пароль будет неправильный, пользователь всё равно создастся. Да и вообще хуйня какая-то эти менеджеры

        foreach (var identityResult in validationResult)
        {
            if (!identityResult.Succeeded)
            {
                throw new InvalidUserException(identityResult.ErrorsToString());
            }
        }
        
        var user = _mapper.Map<User>(accountCreateDto);
        user.UserName = Guid.NewGuid().ToString(); // Crutch: UserName is must be unique 

        await _userManager.CreateAsync(user);
        await _context.Customers.AddAsync(new Customer
        {
            Adress = accountCreateDto.Address,
            User = user
        });
            
        var addPasswordResult = await _userManager.AddPasswordAsync(user, accountCreateDto.Password);
        var addToRoleResult = await _userManager.AddToRoleAsync(user, RoleType.Customer.ToString());

        if (!addPasswordResult.Succeeded)
        {
            throw new InvalidUserException(addPasswordResult.ErrorsToString());
        }

        if (!addToRoleResult.Succeeded)
        {
            throw new InvalidUserException(addToRoleResult.ErrorsToString());
        }
        
        var claims = await GenerateUserClaimsAsync(user);
        var tokenDto = _jwtService.GenerateToken(claims);
        user.RefreshToken = tokenDto.RefreshToken;
        await _userManager.UpdateAsync(user);
        return tokenDto;
    }

    public async Task<TokenDto> RefreshTokenAsync(TokenDto tokenDto)
    {
        var claimsPrincipal = GetPrincipalFromExpiredToken(tokenDto.AccessToken);
        var user = await FindUserByClaimsPrincipal(claimsPrincipal);
        return await RefreshTokenAsync(tokenDto.RefreshToken, user, claimsPrincipal.Claims.ToList());
    }

    public async Task LogOutAsync(ClaimsPrincipal claimsPrincipal)
    {
        var user = await FindUserByClaimsPrincipal(claimsPrincipal);
        user.RefreshToken = null;
        await _userManager.UpdateAsync(user);
    }

    private async Task<TokenDto> RefreshTokenAsync(string refreshToken, User user, List<Claim> claims)
    {
        if (refreshToken != user.RefreshToken)
        {
            throw new InvalidAccessOrRefreshToken();
        }

        var newTokenDto = _jwtService.GenerateToken(claims);
        user.RefreshToken = newTokenDto.RefreshToken;
        await _userManager.UpdateAsync(user);

        return newTokenDto;
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string? token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = JwtConfigurations.GetSymmetricSecurityKey(), //todo: не уверен, что это работает верно
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;
        ClaimsPrincipal principal;
        
        try
        {
            principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new InvalidTokenException(e);
        }
        
        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            throw new InvalidTokenException();

        if (principal == null)
        {
            throw new InvalidAccessOrRefreshToken();
        }
        
        return principal;
    }

    private async Task<User> FindUserByCredentialsAsync(CredentialsDto credentialsDto)
    {
        var user = await _userManager.FindByEmailAsync(credentialsDto.Email);

        if (user == null || !await _userManager.CheckPasswordAsync(user, credentialsDto.Password))
        {
            throw new InvalidEmailOrPasswordException();
        }

        return user;
    }

    private async Task<User> FindUserByClaimsPrincipal(ClaimsPrincipal claimsPrincipal)
    {
        var userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            throw new InvalidTokenException();
        }

        var user = await _userManager.FindByIdAsync(userId.Value);

        if (user == null)
        {
            throw new InvalidTokenException();
        }

        return user;
    }

    private async Task<List<Claim>> GenerateUserClaimsAsync(User user)
    {
        var defaultClaims = GetDefaultClaim(user);
        var userClaims = await GetUserClaimsAsync(user);
        var roleClaims = await GetRoleClaimsAsync(user);
        
        return defaultClaims.Concat(userClaims).Concat(roleClaims).ToList();
    }
    
    private async Task<IEnumerable<Claim>> GetUserClaimsAsync(User user) => await _userManager.GetClaimsAsync(user);

    private async Task<IEnumerable<Claim>> GetRoleClaimsAsync(User user)
    {
        List<Claim> roleClaims = new ();
        var roleNames = await _userManager.GetRolesAsync(user);
        
        foreach (var roleName in roleNames)
        {
            roleClaims.Add(new Claim(ClaimTypes.Role, roleName));
            
            var role = await _roleManager.FindByNameAsync(roleName);
            var claimsAsync = await _roleManager.GetClaimsAsync(role);
            roleClaims.AddRange(claimsAsync);
        }

        return roleClaims;
    }

    private IEnumerable<Claim> GetDefaultClaim(User user)
    {
        return new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString())
        };
    }

    private async Task<IEnumerable<IdentityResult>> ValidateUserAsync(AccountCreateDto accountCreateDto)
    {
        var user = _mapper.Map<User>(accountCreateDto);
        user.UserName = Guid.NewGuid().ToString(); // Crutch: UserName is must be unique 

        List<IdentityResult> identityResults = new();

        var userValidators = _userManager.UserValidators;
        foreach (var userValidator in userValidators)
        {
            identityResults.Add(await userValidator.ValidateAsync(_userManager, user));
        }

        var passwordValidators = _userManager.PasswordValidators;
        foreach (var passwordValidator in passwordValidators)
        {
            identityResults.Add(await passwordValidator.ValidateAsync(_userManager, user, accountCreateDto.Password));
        }

        return identityResults;
    }
}