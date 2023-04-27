using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAggregator.Common.IServices;
using RestaurantAggregator.Common.Models.Dto;
using RestaurantAggregator.DAL.DbContexts;

namespace RestaurantAggregator.BL.Services;

public class RestaurantService : IRestaurantService
{
    private readonly ApplicationDbContext _context;

    private readonly IMapper _mapper;

    public RestaurantService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RestaurantDto>> FetchRestaurants(string? contains, int? page)
    {
        var restaurants = await _context.Restaurants
            .Where(restaurant => restaurant.Name.Contains(contains ?? ""))
            .ToListAsync();
        
        return restaurants.Select(restaurant => _mapper.Map<RestaurantDto>(restaurant));
    }
}