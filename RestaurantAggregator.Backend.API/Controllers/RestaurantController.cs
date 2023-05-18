using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantAggregator.Backend.API.Models.Restaurant;
using RestaurantAggregator.Common.IServices;
using RestaurantAggregator.Common.Models;

namespace RestaurantAggregator.Backend.API.Controllers;

[ApiController]
[Route("api/restaurant")]
public class RestaurantController : ControllerBase
{
    private readonly IRestaurantService _restaurantService;

    private readonly IMapper _mapper;

    public RestaurantController(IRestaurantService restaurantService, IMapper mapper)
    {
        _restaurantService = restaurantService;
        _mapper = mapper;
    }

    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="500">InternalServerError</response>
    [HttpGet]
    public async Task<IActionResult> FetchRestaurants(string? contains = "", int page = 1)
    {
        var restaurants = await _restaurantService.FetchRestaurantsAsync(contains, page);
        return Ok(_mapper.Map<PagedEnumerable<RestaurantModel>>(restaurants));
    }
}