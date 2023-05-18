using System.Security.Claims;
using RestaurantAggregator.BL.Extensions;
using RestaurantAggregator.Common.Exceptions;
using RestaurantAggregator.Common.IServices;
using RestaurantAggregator.Common.Models.Dto;
using RestaurantAggregator.DAL.DbContexts;

namespace RestaurantAggregator.BL.Services;

public class DishStaffService : IDishStaffService
{
    private readonly ApplicationDbContext _context;

    private readonly IDishService _dishService;

    public DishStaffService(ApplicationDbContext context, IDishService dishService)
    {
        _context = context;
        _dishService = dishService;
    }

    public Task<DishPagedListDto> FetchManagerAllDishes(ClaimsPrincipal claimsPrincipal, DishStaffOptionsDto dishStaffOptionsDto)
    {
        var cook = _context.Cooks.SingleOrDefault(cook => cook.Id == claimsPrincipal.GetNameIdentifier());

        if (cook == null)
        {
            throw new DishNotFoundException(); // crutch 
        }
        
        // _context.Dishes.Select(dish => dish.Restaurant.Id == cook.Restaurant.Id && dish.S);
        throw new NotImplementedException();
    }

    public Task<DishPagedListDto> FetchCookAllDishes(ClaimsPrincipal claimsPrincipal, DishStaffOptionsDto dishStaffOptionsDto)
    {
        throw new NotImplementedException();
    }

    public Task<DishPagedListDto> FetchCourierAllDishes(ClaimsPrincipal claimsPrincipal, DishStaffOptionsDto dishStaffOptionsDto)
    {
        throw new NotImplementedException();
    }
    
    
}