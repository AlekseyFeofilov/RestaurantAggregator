using Microsoft.EntityFrameworkCore;
using RestaurantAggregator.Backend.Common.Exceptions;
using RestaurantAggregator.Backend.DAL.DbContexts;
using RestaurantAggregator.Backend.DAL.Entities;
using RestaurantAggregator.Backend.DAL.IRepositories;

namespace RestaurantAggregator.Backend.BL.Repositories;

public class RestaurantRepository : IRestaurantRepository
{
    private readonly ApplicationDbContext _context;

    public RestaurantRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Restaurant> FetchRestaurantAsync(Guid restaurantId)
    {
        var restaurant = await _context.Restaurants.SingleOrDefaultAsync(x => x.Id == restaurantId);

        if (restaurant == null)
        {
            throw new RestaurantNotFoundException();
        }

        return restaurant;
    }
}