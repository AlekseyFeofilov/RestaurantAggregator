using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAggregator.Common.Configurations;
using RestaurantAggregator.Common.Exceptions;
using RestaurantAggregator.Common.Extensions;
using RestaurantAggregator.Common.IServices;
using RestaurantAggregator.Common.Models;
using RestaurantAggregator.Common.Models.Dto;
using RestaurantAggregator.Common.Models.Dto.Restaurant;
using RestaurantAggregator.DAL.DbContexts;
using RestaurantAggregator.DAL.Entities;

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

    public async Task<PagedEnumerable<RestaurantDto>> FetchRestaurantsAsync(string? contains, int page = 1)
    {
        var restaurants = _context.Restaurants
            .Where(restaurant => restaurant.Name.Contains(contains ?? ""));

        var pagedRestaurants = restaurants.GetPagedQueryable(page, AppConfigurations.PageSize);
        var restaurantDtos = new PagedEnumerable<RestaurantDto>(
            await pagedRestaurants.Items.Select(restaurant => _mapper.Map<RestaurantDto>(restaurant))
                .ToListAsync(),
            pagedRestaurants.Pagination
        );
        
        return restaurantDtos;
    }

    public async Task<RestaurantDto> FetchRestaurantDetailsAsync(Guid id)
    {
        var restaurant = await _context.Restaurants.SingleOrDefaultAsync(x => x.Id == id);

        if (restaurant == null)
        {
            throw new RestaurantNotFoundException();
        }

        return _mapper.Map<RestaurantDto>(restaurant);
    }

    public async Task CreateRestaurantAsync(CreateRestaurantDto createRestaurantDto)
    {
        var restaurant = _mapper.Map<Restaurant>(createRestaurantDto);
        await _context.Restaurants.AddAsync(restaurant);
        await _context.SaveChangesAsync();
    }

    public async Task ModifyRestaurantAsync(ModifyRestaurantDto modifyRestaurantDto)
    {
        var restaurant = await _context.Restaurants.SingleOrDefaultAsync(x => x.Id == modifyRestaurantDto.Id);

        if (restaurant == null)
        {
            throw new RestaurantNotFoundException();
        }

        restaurant.Name = modifyRestaurantDto.Name;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteRestaurantAsync(Guid id)
    {
        var restaurant = await _context.Restaurants.SingleOrDefaultAsync(x => x.Id == id);

        if (restaurant == null)
        {
            throw new RestaurantNotFoundException();
        }

        _context.Restaurants.Remove(restaurant);
        await _context.SaveChangesAsync();
    }
}