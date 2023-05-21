using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAggregator.Backend.Common.Configurations;
using RestaurantAggregator.Backend.Common.Dto;
using RestaurantAggregator.Backend.Common.Dto.Dish;
using RestaurantAggregator.Backend.Common.Enums;
using RestaurantAggregator.Backend.Common.IServices;
using RestaurantAggregator.Common.Models.Enums;

namespace RestaurantAggregator.Backend.API.Controllers;

[ApiController]
[Route("api")]
public class DishStaffController : ControllerBase
{
    private readonly IDishStaffService _dishStaffService;

    public DishStaffController(IDishStaffService dishStaffService)
    {
        _dishStaffService = dishStaffService;
    }

    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">InternalServerError</response>
    [Produces(AppConfigurations.ResponseContentType)]
    [ProducesResponseType(typeof(IEnumerable<DishDto>), StatusCodes.Status200OK)]
    [HttpGet, Route("manager/dish")]
    [Authorize]
    public async Task<IActionResult> FetchManagerAllDishes(
        DishStaffSorting dishStaffSorting,
        int page = 1)
    {
        var dishOptions = new DishStaffOptionsDto(dishStaffSorting, page);
        return Ok(await _dishStaffService.FetchManagerAllDishes(User, dishOptions));
    }

    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">InternalServerError</response>
    [Produces(AppConfigurations.ResponseContentType)]
    [ProducesResponseType(typeof(IEnumerable<DishDto>), StatusCodes.Status200OK)]
    [HttpGet, Route("cook/dish")]
    [Authorize]
    public async Task<IActionResult> FetchCookAllDishes(
        DishStaffSorting dishStaffSorting,
        int page = 1)
    {
        var dishOptions = new DishStaffOptionsDto(dishStaffSorting, page);
        return Ok(await _dishStaffService.FetchCookAllDishes(User, dishOptions));
    }

    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">InternalServerError</response>
    [Produces(AppConfigurations.ResponseContentType)]
    [ProducesResponseType(typeof(IEnumerable<DishDto>), StatusCodes.Status200OK)]
    [HttpGet, Route("courier/dish")]
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> FetchCourierAllDishes(
        DishStaffSorting dishStaffSorting,
        int page = 1)
    {
        var dishOptions = new DishStaffOptionsDto(dishStaffSorting, page);
        return Ok(await _dishStaffService.FetchCourierAllDishes(User, dishOptions));
    }
}