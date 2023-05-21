using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAggregator.Auth.Common.IServices;
using RestaurantAggregator.Auth.Common.Models.Dtos;
using RestaurantAggregator.Backend.Common.Configurations;

namespace RestaurantAggregator.Auth.API.Controllers;

[ApiController]
[Route("api/account")]
[Produces(AppConfigurations.ResponseContentType)]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="500">InternalServerError</response>
    [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
    [HttpPost, Route("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] AccountCreateDto accountCreateDto)
    {
        return Ok(await _authService.SignUpAsync(accountCreateDto));
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