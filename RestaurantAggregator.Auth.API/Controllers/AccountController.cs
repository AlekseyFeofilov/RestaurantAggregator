using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAggregator.Auth.Common.IServices;
using RestaurantAggregator.Auth.Common.Models.Dtos;
using RestaurantAggregator.Backend.Common.Configurations;

namespace RestaurantAggregator.Auth.API.Controllers;

[ApiController]
[Route("api/account")]
[Produces(AppConfigurations.ResponseContentType)]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    /// <response code="200">Success</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">InternalServerError</response>
    [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> FetchProfileInfo() //todo по идее надо сделать 4 разных эндпоинта для каждой роли, потому что в будущем колчеситво различной информации может быть гораздо больше, чем просто Adress у пользователя
    {
        return Ok(await _accountService.FetchProfileInfo(User));
    }

    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">InternalServerError</response>
    [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
    [HttpPatch]
    [Authorize]
    public async Task<IActionResult> ModifyProfileInfo([FromBody] AccountModifyDto accountModifyDto)
    {
        return Ok(await _accountService.ModifyProfileInfo(User, accountModifyDto));
    }
    
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">InternalServerError</response>
    [HttpPut, Route("security/password")]
    [Authorize]
    public async Task<IActionResult> ChangePassword([FromBody] PasswordModifyDto passwordModifyDto)
    {
        await _accountService.ChangePassword(User, passwordModifyDto);
        return Ok();
    }
}