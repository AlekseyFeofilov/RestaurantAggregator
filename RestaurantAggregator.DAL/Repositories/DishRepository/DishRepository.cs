using Microsoft.EntityFrameworkCore;
using RestaurantAggregator.Common.Exceptions;
using RestaurantAggregator.Common.Models;
using RestaurantAggregator.Common.Models.Dto;
using RestaurantAggregator.Common.Models.Enums;
using RestaurantAggregator.DAL.DbContexts;
using RestaurantAggregator.DAL.Entities;
using RestaurantAggregator.DAL.Models;

namespace RestaurantAggregator.DAL.Repositories.DishRepository;

public class DishRepository : IDishRepository
{
    private readonly ApplicationDbContext _context;

    public DishRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Dish>> FetchAllDishes(FetchDishOptions fetchDishOptions)
    {
        var dishes = _context.Dishes.Where(dish => dish.Restaurant.Id == fetchDishOptions.RestaurantId).AsQueryable();

        if (fetchDishOptions.MenuId != null)
        {
            dishes = dishes.Where(dish => dish.Menus.Any(menu => menu.Id == fetchDishOptions.MenuId));
        }
        
        dishes = GetVegetarian(dishes, fetchDishOptions.Vegetarian);
        dishes = GetCategory(dishes, fetchDishOptions.Categories);
        dishes = Sort(dishes, fetchDishOptions.Sorting);
        var take = GetDishPageCount(dishes.Count(), fetchDishOptions.Skip, fetchDishOptions.Take);

        return await GetDishPage(dishes, fetchDishOptions.Skip, take);
    }

    private static IQueryable<Dish> GetVegetarian(IQueryable<Dish> dishes, bool available = true)
    {
        return available ? dishes.Where(x => x.Vegetarian) : dishes;
    }

    private static IQueryable<Dish> GetCategory(IQueryable<Dish> dishes, DishCategory[]? category = null)
    {
        return !(category == null || !category.Any<DishCategory>())
            ? dishes.Where(x => category != null && category.Contains(x.Category))
            : dishes;
    }

    private static IQueryable<Dish> Sort(IQueryable<Dish> dishes, DishSorting? sorting = null)
    {
        return sorting switch
        {
            DishSorting.NameAsc => dishes.OrderBy(x => x.Name),
            DishSorting.NameDesc => dishes.OrderByDescending(x => x.Name),
            DishSorting.PriceAsc => dishes.OrderBy(x => x.Price),
            DishSorting.PriceDesc => dishes.OrderByDescending(x => x.Price),
            DishSorting.RatingAsc => dishes.OrderBy(x => x.Reviews.Average(r => r.Rating)),
            DishSorting.RatingDesc => dishes.OrderByDescending(x => x.Reviews.Average(r => r.Rating)),
            null => dishes,
            _ => throw new ArgumentOutOfRangeException(nameof(sorting), sorting, null)
        };
    }

    private async Task<List<Dish>> GetDishPage(IQueryable<Dish> dishes, int skip, int take)
    {
        var test = await dishes
            .Skip(skip)
            .Take(take)
            .Include(x => x.Reviews)
            .ToListAsync();

        return test;
    }

    private int GetDishPageCount(int dishCount, int skip, int take)
    {
        if (dishCount < skip + take) //todo maybe кидать 404, если dishCount <= skip
        {
            return Math.Max(0, dishCount - skip);
        }

        return take;
    }
}