using System.Security.Claims;
using AutoMapper;
using RestaurantAggregator.Backend.Common.Configurations;
using RestaurantAggregator.Backend.Common.Dtos.Dish;
using RestaurantAggregator.Backend.Common.IServices;
using RestaurantAggregator.Backend.DAL.Entities;
using RestaurantAggregator.Backend.DAL.IRepositories;
using RestaurantAggregator.Common.Dtos;
using RestaurantAggregator.Common.Extensions;

namespace RestaurantAggregator.Backend.BL.Services;

public class DishServices : IDishService
{
    private readonly IMapper _mapper;

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
        var pagedDishes = await _dishRepository.FetchAllDishesAsync(dishOptions, onlyActive);
        var pagedDishDtos = new PagedEnumerable<DishDto>(
            pagedDishes.Items.Select(x => _mapper.Map<DishDto>(x)),
            pagedDishes.Pagination
        );

        return pagedDishDtos;
    }

    public Task<PagedEnumerable<DishDto>> FetchAllAsync(DishOptions dishOptions)
    {
        return FetchAllAsync(dishOptions, true);
    }

    public Task<PagedEnumerable<DishDto>> FetchAllAsync(ClaimsPrincipal claimsPrincipal, DishOptions dishOptions, bool onlyActive = true)
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

    public async Task<Guid> ModifyAsync(DishModifyDto dishModifyDto)
    {
        var restaurant =  (await _dishRepository.FetchDishAsync(dishModifyDto.Id)).Restaurant;
        var dish = _mapper.Map<Dish>(dishModifyDto);
        dish.Restaurant = restaurant;

        return await _dishRepository.ModifyDishAsync(dish);
    }

    public Task DeleteAsync(Guid dishId)
    {
        return _dishRepository.DeleteDishAsync(dishId);
    }
}