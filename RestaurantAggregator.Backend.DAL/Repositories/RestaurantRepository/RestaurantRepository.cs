using Microsoft.EntityFrameworkCore;
using RestaurantAggregator.Common.Exceptions;
using RestaurantAggregator.DAL.DbContexts;
using RestaurantAggregator.DAL.Entities;

namespace RestaurantAggregator.DAL.Repositories.RestaurantRepository;

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