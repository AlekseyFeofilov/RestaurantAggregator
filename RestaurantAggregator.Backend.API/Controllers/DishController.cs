using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAggregator.API.Models.Dish;
using RestaurantAggregator.Common.Configurations;
using RestaurantAggregator.Common.IServices;
using RestaurantAggregator.Common.Models;
using RestaurantAggregator.Common.Models.Dto;
using RestaurantAggregator.Common.Models.Dto.Dish;
using RestaurantAggregator.Common.Models.Enums;

namespace RestaurantAggregator.API.Controllers;

[ApiController]
[Route("api/dish")]
public class DishController : ControllerBase
{
    private readonly IDishService _dishService;

    private readonly IMapper _mapper;

    public DishController(IDishService dishService, IMapper mapper)
    {
        _dishService = dishService;
        _mapper = mapper;
    }

    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">InternalServerError</response>
    [Produces(AppConfigurations.ResponseContentType)]
    [ProducesResponseType(typeof(IEnumerable<DishModel>), StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<IActionResult> FetchAllDishes(
        [Required] Guid restaurantId,
        Guid? menuId = null,
        [FromQuery] DishCategory[]? categories = null,
        bool vegetarian = false,
        DishSorting? sorting = null,
        int page = 1)
    {
        var dishOptions = new DishOptions(restaurantId, menuId, categories, vegetarian, sorting, page);
        var dishPagedListDto = await _dishService.FetchAllDishesAsync(dishOptions);
        
        return Ok(_mapper.Map<PagedEnumerable<DishModel>>(dishPagedListDto));
    }

    /// <summary>
    /// Get information about concrete dish
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">InternalServerError</response>
    [Produces(AppConfigurations.ResponseContentType)]
    [ProducesResponseType(typeof(DishModel), StatusCodes.Status200OK)]
    [HttpGet, Route("{dishId:guid}")]
    public async Task<IActionResult> FetchDish(Guid dishId)
    {
        var dishDto = await _dishService.FetchDishAsync(dishId);
        return Ok(_mapper.Map<DishModel>(dishDto));
    }

    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    /// <response code="500">InternalServerError</response>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateDish(DishCreateModel dishCreateModel)
    {
        var dishCreateDto = _mapper.Map<DishCreateDto>(dishCreateModel);
        await _dishService.CreateDishAsync(dishCreateDto);
        return Ok();
    }
    
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    /// <response code="500">InternalServerError</response>
    [HttpPut] // todo поприкалываться с тем, чтобы сделать patch
    [Authorize]
    public async Task<IActionResult> ModifyDish(DishModifyModel dishModifyModel) {
        var dishCreateDto = _mapper.Map<DishModifyDto>(dishModifyModel);
        await _dishService.ModifyDishAsync(dishCreateDto);
        return Ok();
    }
    
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    /// <response code="500">InternalServerError</response>
    [HttpDelete, Route("{dishId:guid}")]
    [Authorize]
    public async Task<IActionResult> DeleteDish(Guid dishId) {
        await _dishService.DeleteDishAsync(dishId);
        return Ok();
    }
}