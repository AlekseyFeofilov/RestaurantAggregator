using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantAggregator.API.Controllers;

[ApiController]
[Route("api/manager/menu")]
public class ManagerMenuController : ControllerBase
{
    
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    /// <response code="500">InternalServerError</response>
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> FetchManagerMenus([FromQuery] Guid restaurantId, int? page = 1) {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }
    
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    /// <response code="500">InternalServerError</response>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateMenu() {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }
    
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    /// <response code="500">InternalServerError</response>
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> ModifyMenu() {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }
    
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    /// <response code="500">InternalServerError</response>
    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteMenu() {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }

    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    /// <response code="500">InternalServerError</response>
    [HttpPost, Route("{menuId:guid}/dish/{dishId:guid}")]
    [Authorize] 
    public async Task<IActionResult> AddDish(Guid menuId, Guid dishId) {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }

    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    /// <response code="500">InternalServerError</response>
    [HttpDelete, Route("{menuId:guid}/dish/{dishId:guid}")]
    [Authorize] 
    public async Task<IActionResult> DeleteDish(Guid menuId, Guid dishId) {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }
}