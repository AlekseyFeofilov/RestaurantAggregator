using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAggregator.Auth.API.Models.Account;
using RestaurantAggregator.Auth.Common.Dtos;
using RestaurantAggregator.Auth.Common.Dtos.Account;
using RestaurantAggregator.Auth.Common.IServices;
using RestaurantAggregator.Backend.Common.Configurations;

namespace RestaurantAggregator.Auth.API.Controllers;

[ApiController]
[Route("api/account")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    private readonly IMapper _mapper;

    public AuthController(IAuthService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }

    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="500">InternalServerError</response>
    [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
    [HttpPost, Route("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] AccountRegisterModel accountRegisterModel)
    {
        return Ok(await _authService.SignUpAsync(_mapper.Map<AccountCreateDto>(accountRegisterModel)));
    }

    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="500">InternalServerError</response>
    [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
    [HttpPost, Route("log-in")]
    public async Task<IActionResult> LogIn([FromBody] CredentialsDto credentialsDto)
    {
        return Ok(await _authService.LogInAsync(credentialsDto));
    }

    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="500">InternalServerError</response>
    [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
    [HttpPost, Route("token")]
    public async Task<IActionResult> RefreshToken([FromBody] TokenDto tokenDto)
    {
        return Ok(await _authService.RefreshTokenAsync(tokenDto));
    }

    /// <response code="200">Success</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">InternalServerError</response>
    [HttpPost, Route("log-out")]
    [Authorize]
    public async Task<IActionResult> LogOut()
    {
        await _authService.LogOutAsync(User);
        return Ok();
    }
}