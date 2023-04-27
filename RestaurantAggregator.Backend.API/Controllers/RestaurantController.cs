using Microsoft.AspNetCore.Mvc;
using RestaurantAggregator.Common.IServices;

namespace RestaurantAggregator.API.Controllers;

[ApiController]
[Route("api/restaurant")]
public class RestaurantController : ControllerBase
{
    private readonly IRestaurantService _restaurantService;

    public RestaurantController(IRestaurantService restaurantService)
    {
        _restaurantService = restaurantService;
    }

    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="500">InternalServerError</response>
    [HttpGet]
    public async Task<IActionResult> FetchRestaurants(string? contains = "", int? page = 1)
    {
        return Ok(await _restaurantService.FetchRestaurants(contains, page));
    }
}