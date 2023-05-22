using Microsoft.AspNetCore.Mvc;
using RestaurantAggregator.Backend.Common.IServices;

namespace RestaurantAggregator.Backend.API.Controllers.CustomerControllers;

[ApiController]
[Route("api/menu")]
public class MenuController : ControllerBase
{
    private readonly IMenuService _menuService;

    public MenuController(IMenuService menuService)
    {
        _menuService = menuService;
    }

    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="500">InternalServerError</response>
    [HttpGet]
    public async Task<IActionResult> FetchMenus([FromQuery] Guid restaurantId, int? page = 1)
    {
        return Ok(await _menuService.FetchMenus(restaurantId, page));
    }
}