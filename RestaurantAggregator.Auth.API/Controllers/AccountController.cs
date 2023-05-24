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
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    private readonly IMapper _mapper;

    public AccountController(IAccountService accountService, IMapper mapper)
    {
        _accountService = accountService;
        _mapper = mapper;
    }

    /// <response code="200">Success</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">InternalServerError</response>
    [ProducesResponseType(typeof(AccountModel), StatusCodes.Status200OK)]
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> FetchProfileInfo() //todo по идее надо сделать 4 разных эндпоинта для каждой роли, потому что в будущем колчеситво различной информации может быть гораздо больше, чем просто Adress у пользователя
    {
        return Ok(_mapper.Map<AccountModel>(await _accountService.FetchProfileInfo(User)));
    }

    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">InternalServerError</response>
    [ProducesResponseType(typeof(AccountModel), StatusCodes.Status200OK)]
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> ModifyProfileInfo([FromBody] AccountModifyModel accountModifyModel)
    {
        return Ok(_mapper.Map<AccountModel>(await _accountService.ModifyProfileInfo(User, _mapper.Map<AccountModifyDto>(accountModifyModel))));
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