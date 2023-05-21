using RestaurantAggregator.Backend.Common.Dto.Restaurant;
using RestaurantAggregator.Common.Models;

namespace RestaurantAggregator.Backend.Common.IServices;

public interface IRestaurantService
{
    Task<PagedEnumerable<RestaurantDto>> FetchRestaurantsAsync(string? contains, int page);

    Task<RestaurantDto> FetchRestaurantDetailsAsync(Guid id);

    Task CreateRestaurantAsync(CreateRestaurantDto createRestaurantDto);
    
    Task ModifyRestaurantAsync(ModifyRestaurantDto modifyRestaurantDto);
    
    Task DeleteRestaurantAsync(Guid id);
}