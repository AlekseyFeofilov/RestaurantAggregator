using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantAggregator.API.Controllers;

[ApiController]
[Route("api/menu")]
public class MenuController : ControllerBase
{
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="500">InternalServerError</response>
    [HttpGet]
    public async Task<IActionResult> FetchMenus([FromQuery] Guid restaurantId, int? page = 1) {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }
}