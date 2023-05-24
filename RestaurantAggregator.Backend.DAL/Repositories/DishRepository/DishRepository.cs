using Microsoft.EntityFrameworkCore;
using RestaurantAggregator.Backend.Common.Configurations;
using RestaurantAggregator.Backend.Common.Dtos.Dish;
using RestaurantAggregator.Backend.Common.Exceptions;
using RestaurantAggregator.Backend.Common.Exceptions.BadRequestExceptions;
using RestaurantAggregator.Backend.Common.Exceptions.NotFoundException;
using RestaurantAggregator.Backend.DAL.DbContexts;
using RestaurantAggregator.Backend.DAL.Entities;
using RestaurantAggregator.Backend.DAL.Repositories.MenuRepository;
using RestaurantAggregator.Backend.DAL.Repositories.RestaurantRepository;
using RestaurantAggregator.Common.Dtos;
using RestaurantAggregator.Common.Dtos.Enums;
using RestaurantAggregator.Common.Extensions;

namespace RestaurantAggregator.Backend.DAL.Repositories.DishRepository;

public class DishRepository : IDishRepository
{
    private readonly ApplicationDbContext _context;

    private readonly IMenuRepository _menuRepository;

    private readonly IRestaurantRepository _restaurantRepository;

    public DishRepository(ApplicationDbContext context, IMenuRepository menuRepository,
        IRestaurantRepository restaurantRepository)
    {
        _context = context;
        _menuRepository = menuRepository;
        _restaurantRepository = restaurantRepository;
    }

    public async Task<PagedEnumerable<Dish>> FetchAllDishesAsync(DishOptions dishOptions,
        bool onlyActive = true)
    {
        IQueryable<Dish> dishes;

        if (dishOptions.MenuId != null)
        {
            var menu = _menuRepository.FetchDetails((Guid)dishOptions.MenuId);

            if (menu.Restaurant.Id != dishOptions.RestaurantId)
            {
                throw new MenuIsNotExistInRestaurant();
            }

            if (!menu.Active && onlyActive)
            {
                throw new MenuNotFoundException();
            }

            dishes = menu.Dishes.Where(x => !x.Deleted && (!onlyActive || x.Active)).AsQueryable();
        }
        else
        {
            if ((await _restaurantRepository.FetchRestaurantAsync(dishOptions.RestaurantId)) == null)
            {
                throw new RestaurantNotFoundException();
            }

            dishes = _context.Dishes.Where(dish =>
                dish.Restaurant.Id == dishOptions.RestaurantId 
                && !dish.Deleted 
                && (!onlyActive || dish.Active)
            ).AsQueryable();
        }

        dishes = GetVegetarian(dishes, dishOptions.Vegetarian);
        dishes = GetCategory(dishes, dishOptions.Categories);
        dishes = Sort(dishes, dishOptions.Sorting);

        var pagedDishes = dishes.GetPagedQueryable(dishOptions.Page, AppConfigurations.PageSize);
        var pagedEnumerableDishes = new PagedEnumerable<Dish>(
            await pagedDishes.Items!
                .Include(x => x.Restaurant)
                .ToListAsync(),
            pagedDishes.Pagination
        );

        return pagedEnumerableDishes;
    }

    public async Task<Dish> FetchDishAsync(Guid dishId, bool onlyActive = true)
    {
        var dish = await _context.Dishes
            .Include(x => x.Restaurant)
            .SingleOrDefaultAsync(x => x.Id == dishId);

        if (dish == null) throw new DishNotFoundException();
        if (dish.Deleted || !dish.Active && onlyActive) throw new DishNotFoundException();

        return dish;
    }

    public async Task CreateDishAsync(Dish dish)
    {
        await _context.Dishes.AddAsync(dish);
        await _context.SaveChangesAsync();
    }

    public async Task<Guid> ModifyDishAsync(Dish dish)
    {
        var oldDish = await FetchDishAsync(dish.Id);
        dish.Id = Guid.NewGuid();
        dish.Active = true;
        
        await _context.Dishes.AddAsync(dish);

        if (!(await _context.Orders.AnyAsync(order => order.DishBaskets.Any(cartDish => cartDish.Dish.Id == oldDish.Id))))
        {
            _context.Dishes.Remove(oldDish);
        }
        else
        {
            oldDish.Deleted = true;   
        }

        await _context.SaveChangesAsync();

        return dish.Id;
    }

    public async Task DeleteDishAsync(Guid dishId)
    {
        (await FetchDishAsync(dishId)).Deleted = true;
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