using System.Security.Claims;
using RestaurantAggregator.Common.Models;
using RestaurantAggregator.Common.Models.Dto;
using RestaurantAggregator.Common.Models.Dto.Restaurant;

namespace RestaurantAggregator.Common.IServices;

public interface IRestaurantService
{
    Task<PagedEnumerable<RestaurantDto>> FetchRestaurantsAsync(string? contains, int page);

    Task<RestaurantDto> FetchRestaurantDetailsAsync(Guid id);

    Task CreateRestaurantAsync(CreateRestaurantDto createRestaurantDto);
    
    Task ModifyRestaurantAsync(ModifyRestaurantDto modifyRestaurantDto);
    
    Task DeleteRestaurantAsync(Guid id);
}