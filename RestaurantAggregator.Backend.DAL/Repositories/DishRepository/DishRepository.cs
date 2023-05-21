using Microsoft.EntityFrameworkCore;
using RestaurantAggregator.Backend.Common.Configurations;
using RestaurantAggregator.Backend.Common.Exceptions;
using RestaurantAggregator.Backend.DAL.DbContexts;
using RestaurantAggregator.Backend.DAL.Entities;
using RestaurantAggregator.Backend.DAL.Models;
using RestaurantAggregator.Common.Extensions;
using RestaurantAggregator.Common.Models;
using RestaurantAggregator.Common.Models.Enums;

namespace RestaurantAggregator.Backend.DAL.Repositories.DishRepository;

public class DishRepository : IDishRepository
{
    private readonly ApplicationDbContext _context;

    public DishRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PagedEnumerable<Dish>>
        FetchAllDishesAsync(
            FetchDishOptions fetchDishOptions) //todo кидать bad request если это меню не из указанного ресторана
    {
        var dishes = fetchDishOptions.MenuId == null
            ? _context.Dishes.Where(dish => dish.Restaurant.Id == fetchDishOptions.RestaurantId).AsQueryable()
            : _context.Dishes.Where(dish => dish.Menus.Any(menu => menu.Id == fetchDishOptions.MenuId)).AsQueryable();

        dishes = GetVegetarian(dishes, fetchDishOptions.Vegetarian);
        dishes = GetCategory(dishes, fetchDishOptions.Categories);
        dishes = Sort(dishes, fetchDishOptions.Sorting);

        var pagedDishes = dishes.GetPagedQueryable(fetchDishOptions.Page, AppConfigurations.PageSize);
        var pagedEnumerableDishes = new PagedEnumerable<Dish>(
            await pagedDishes.Items!
                .Include(x => x.Restaurant)
                .ToListAsync(),
            pagedDishes.Pagination
        );

        return pagedEnumerableDishes;
    }

    public async Task<Dish> FetchDishAsync(Guid dishId)
    {
        var dish = await _context.Dishes
            .Include(x => x.Restaurant)
            .SingleOrDefaultAsync(x => x.Id == dishId);

        if (dish == null) throw new DishNotFoundException();

        return dish;
    }

    public async Task CreateDishAsync(Dish dish)
    {
        await _context.Dishes.AddAsync(dish);
        await _context.SaveChangesAsync();
    }

    public async Task ModifyDishAsync(Dish dish)
    {
        var oldDish = await FetchDishAsync(dish.Id);

        oldDish.Name = dish.Name;
        oldDish.Description = dish.Description;
        oldDish.Price = dish.Price;
        oldDish.Image = dish.Image;
        oldDish.Vegetarian = dish.Vegetarian;
        oldDish.Category = dish.Category;
        oldDish.Restaurant = dish.Restaurant;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteDishAsync(Guid dishId)
    {
        _context.Dishes.Remove(await FetchDishAsync(dishId));
        await _context.SaveChangesAsync();
    }

    private static IQueryable<Dish> GetVegetarian(IQueryable<Dish> dishes, bool available = true)
    {
        return available ? dishes.Where(x => x.Vegetarian) : dishes;
    }

    private static IQueryable<Dish> GetCategory(IQueryable<Dish> dishes, DishCategory[]? category = null)
    {
        return category == null || !category.Any()
            ? dishes
            : dishes.Where(x => category.Contains(x.Category));
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
}