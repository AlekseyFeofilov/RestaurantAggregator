using Microsoft.AspNetCore.Mvc;

namespace RestaurantAggregator.API.Controllers;

[ApiController]
[Route("api")]
public class OrderStatusController : ControllerBase
{
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="500">InternalServerError</response>
    [HttpGet]
    public async Task<IActionResult> FetchOrderStatuses() {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }

    /// <response code="200">Success</response>
    /// <response code="401">Not Authorized</response>
    /// <response code="403">Forbidden</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">InternalServerError</response>
    [HttpPatch, Route("dish/{dishId::guid}/status")]
    public async Task<IActionResult> ChangeOrderStatus(Guid dishId, [FromBody] Guid statusId) {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }
}