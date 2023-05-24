using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RestaurantAggregator.Backend.Common.Configurations;
using RestaurantAggregator.Backend.Common.Dtos.Restaurant;
using RestaurantAggregator.Backend.Common.Exceptions;
using RestaurantAggregator.Backend.Common.Exceptions.NotFoundException;
using RestaurantAggregator.Backend.Common.IServices;
using RestaurantAggregator.Backend.DAL.DbContexts;
using RestaurantAggregator.Backend.DAL.Entities;
using RestaurantAggregator.Common.Dtos;
using RestaurantAggregator.Common.Extensions;

namespace RestaurantAggregator.Backend.BL.Services;

public class RestaurantService : IRestaurantService
{
    private readonly ApplicationDbContext _context;

    private readonly IMapper _mapper;

    private readonly IOptions<AppConfigurations> _configurations;

    public RestaurantService(ApplicationDbContext context, IMapper mapper, IOptions<AppConfigurations> configurations)
    {
        _context = context;
        _mapper = mapper;
        _configurations = configurations;
    }

    public async Task<PagedEnumerable<RestaurantDto>> FetchRestaurantsAsync(string? contains, int page = 1)
    {
        var restaurants = _context.Restaurants
            .Where(restaurant => restaurant.Name.Contains(contains ?? ""));

        var pagedRestaurants = restaurants.GetPagedQueryable(page, _configurations.Value.PageSize);
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
            throw new RestaurantNotFoundException(id);
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
            throw new RestaurantNotFoundException(modifyRestaurantDto.Id);
        }

        restaurant.Name = modifyRestaurantDto.Name;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteRestaurantAsync(Guid id)
    {
        var restaurant = await _context.Restaurants.SingleOrDefaultAsync(x => x.Id == id);

        if (restaurant == null)
        {
            throw new RestaurantNotFoundException(id);
        }

        _context.Restaurants.Remove(restaurant);
        await _context.SaveChangesAsync();
    }
}