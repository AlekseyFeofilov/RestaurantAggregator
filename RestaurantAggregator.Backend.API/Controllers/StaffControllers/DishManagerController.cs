using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAggregator.Auth.Common.Models.Enums;
using RestaurantAggregator.Backend.API.Models.Dish;
using RestaurantAggregator.Backend.Common.Configurations;
using RestaurantAggregator.Backend.Common.Dto;
using RestaurantAggregator.Backend.Common.Dto.Dish;
using RestaurantAggregator.Backend.Common.IServices;
using RestaurantAggregator.Common.Models;
using RestaurantAggregator.Common.Models.Enums;

namespace RestaurantAggregator.Backend.API.Controllers.StaffControllers;

[ApiController]
[Route("api/manager/dish")] // todo make with configuration
[Authorize(Roles = "Manager")] //todo сделать по-человечески, а не хардкодом
public class DishManagerController : ControllerBase
{
    private readonly IDishService _dishService;

    private readonly IMapper _mapper;

    public DishManagerController(IDishService dishService, IMapper mapper)
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
    public async Task<IActionResult> FetchAllDishes( //todo make ability to chose manager see only active dishes
        Guid? menuId = null,
        [FromQuery] DishCategory[]? categories = null,
        bool vegetarian = false,
        DishSorting? sorting = null,
        int page = 1)
    {
        var dishOptions = new DishOptions(null, menuId, categories, vegetarian, sorting, page); //crutch: сделать разные модельки
        var dishPagedListDto = await _dishService.FetchAllAsync(User, dishOptions, true);
        
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
    [Authorize("RetrieveDish")]
    public async Task<IActionResult> FetchDish(Guid dishId)
    {
        var dishDto = await _dishService.FetchDetailsAsync(dishId, false);
        return Ok(_mapper.Map<DishModel>(dishDto));
    }

    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    /// <response code="500">InternalServerError</response>
    [HttpPost]
    public async Task<IActionResult> CreateDish(DishCreateModel dishCreateModel)
    {
        var dishCreateDto = _mapper.Map<DishCreateDto>(dishCreateModel);
        await _dishService.CreateAsync(User, dishCreateDto);
        return Ok();
    }

    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    /// <response code="500">InternalServerError</response>
    [HttpPut] // todo поприкалываться с тем, чтобы сделать patch
    [Authorize("ModifyDish")]
    public async Task<IActionResult> ModifyDish(DishModifyModel dishModifyModel) { //todo check if reviews are saving after modifying  
        var dishCreateDto = _mapper.Map<DishModifyDto>(dishModifyModel);
        return Ok(await _dishService.ModifyAsync(dishCreateDto));
    }
    
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    /// <response code="500">InternalServerError</response>
    [HttpDelete, Route("{dishId:guid}")]
    [Authorize("DeleteDish")]
    public async Task<IActionResult> DeleteDish(Guid dishId) {
        await _dishService.DeleteAsync(dishId);
        return Ok();
    }
}