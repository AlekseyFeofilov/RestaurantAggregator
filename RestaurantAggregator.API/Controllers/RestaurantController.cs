using Microsoft.AspNetCore.Mvc;

namespace RestaurantAggregator.API.Controllers;

[ApiController]
[Route("api/restaurant")]
public class RestaurantController : ControllerBase
{
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="500">InternalServerError</response>
    [HttpGet]
    public async Task<IActionResult> FetchRestaurants(string? startWith = "", int? page = 1) {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }
    
    
}