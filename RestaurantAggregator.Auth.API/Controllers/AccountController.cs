using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantAggregator.API.Controllers;

[ApiController]
[Route("api/account")]
public class AccountController : ControllerBase
{
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="500">InternalServerError</response>
    [HttpPost, Route("sign-up")]
    public async Task<IActionResult> SignUp()
    {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }

    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="500">InternalServerError</response>
    [HttpPost, Route("sign-in")]
    public async Task<IActionResult> LogIn()
    {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }

    /// <response code="200">Success</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">InternalServerError</response>
    [HttpPost, Authorize, Route("logout")]
    public async Task<IActionResult> LogOut()
    {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }

    /// <response code="200">Success</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">InternalServerError</response>
    [HttpGet, Authorize]
    public async Task<IActionResult> GetProfileInfo()
    {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }

    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">InternalServerError</response>
    [HttpPut, Authorize]
    public async Task<IActionResult> ModifyProfileInfo()
    {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }
    
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">InternalServerError</response>
    [HttpPut, Authorize, Route("password")]
    public async Task<IActionResult> ChangePassword()
    {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }
}