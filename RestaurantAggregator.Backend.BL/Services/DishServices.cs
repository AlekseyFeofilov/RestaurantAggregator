using System.Security.Claims;
using AutoMapper;
using RestaurantAggregator.Backend.Common.Configurations;
using RestaurantAggregator.Backend.Common.Dto;
using RestaurantAggregator.Backend.Common.Dto.Dish;
using RestaurantAggregator.Backend.Common.IServices;
using RestaurantAggregator.Backend.DAL.Entities;
using RestaurantAggregator.Backend.DAL.Models;
using RestaurantAggregator.Backend.DAL.Repositories.DishRepository;
using RestaurantAggregator.Backend.DAL.Repositories.ManagerRepository;
using RestaurantAggregator.Backend.DAL.Repositories.MenuRepository;
using RestaurantAggregator.Backend.DAL.Repositories.RestaurantRepository;
using RestaurantAggregator.Backend.DAL.Repositories.ReviewRepository;
using RestaurantAggregator.Common.Extensions;
using RestaurantAggregator.Common.Models;

namespace RestaurantAggregator.Backend.BL.Services;

public class DishServices : IDishService
{
    private readonly IMapper _mapper;

    private const int PageSize = AppConfigurations.PageSize;

    private readonly IDishRepository _dishRepository;

    private readonly IReviewRepository _reviewRepository;

    private readonly IRestaurantRepository _restaurantRepository;

    private readonly IManagerRepository _managerRepository;

    private readonly IMenuRepository _menuRepository; 

    public DishServices(IMapper mapper, IDishRepository dishRepository, IReviewRepository reviewRepository,
        IRestaurantRepository restaurantRepository, IManagerRepository managerRepository, IMenuRepository menuRepository)
    {
        _mapper = mapper;
        _dishRepository = dishRepository;
        _reviewRepository = reviewRepository;
        _restaurantRepository = restaurantRepository;
        _managerRepository = managerRepository;
        _menuRepository = menuRepository;
    }

    public async Task<PagedEnumerable<DishDto>> FetchAllAsync(DishOptions dishOptions, bool onlyActive = true)
    {
        var fetchDishOptions = _mapper.Map<FetchDishOptions>(dishOptions); //todo remove FetchDishOptions and use DishOptions in Common

        var pagedDishes = await _dishRepository.FetchAllDishesAsync(fetchDishOptions, onlyActive);
        var pagedDishDtos = new PagedEnumerable<DishDto>(
            pagedDishes.Items.Select(x => _mapper.Map<DishDto>(x)),
            pagedDishes.Pagination
        );

        return pagedDishDtos;
    }

    public Task<PagedEnumerable<DishDto>> FetchAllAsync(DishOptions dishOptions)
    {
        return FetchAllAsync(dishOptions, false);
    }

    public Task<PagedEnumerable<DishDto>> FetchAllAsync(ClaimsPrincipal claimsPrincipal, DishOptions dishOptions, bool onlyActive = false)
    {
        var manager = _managerRepository.FetchDetails(claimsPrincipal.GetNameIdentifier());
        dishOptions.RestaurantId = manager.Restaurant.Id;
        return FetchAllAsync(dishOptions, onlyActive);
    }

    public async Task<DishDto> FetchDetailsAsync(Guid dishId, bool onlyActive = true)
    {
        await _reviewRepository.FetchReviews(dishId);
        var dish = await _dishRepository.FetchDishAsync(dishId, onlyActive);

        return _mapper.Map<DishDto>(dish);
    }

    public async Task CreateAsync(ClaimsPrincipal claimsPrincipal, DishCreateDto dishCreateDto)
    {
        var managerId = claimsPrincipal.GetNameIdentifier();
        var manager = _managerRepository.FetchDetails(managerId);
        
        var restaurant = await _restaurantRepository.FetchRestaurantAsync(manager.Restaurant.Id);
        var dish = _mapper.Map<Dish>(dishCreateDto);
        dish.Restaurant = restaurant;

        await _dishRepository.CreateDishAsync(dish);
    }

    public async Task ModifyAsync(DishModifyDto dishModifyDto)
    {
        var restaurant = await _restaurantRepository.FetchRestaurantAsync(dishModifyDto.RestaurantId);
        var dish = _mapper.Map<Dish>(dishModifyDto);
        dish.Restaurant = restaurant;

        await _dishRepository.ModifyDishAsync(dish);
    }

    public Task DeleteAsync(Guid dishId)
    {
        return _dishRepository.DeleteDishAsync(dishId);
    }
}