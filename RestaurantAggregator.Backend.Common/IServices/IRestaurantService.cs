using RestaurantAggregator.Backend.Common.Dtos.Restaurant;
using RestaurantAggregator.Common.Dtos;

namespace RestaurantAggregator.Backend.Common.IServices;

public interface IRestaurantService
{
    Task<PagedEnumerable<RestaurantDto>> FetchRestaurantsAsync(string? contains, int page);

    Task<RestaurantDto> FetchRestaurantDetailsAsync(Guid id);

    Task CreateRestaurantAsync(CreateRestaurantDto createRestaurantDto);
    
    Task ModifyRestaurantAsync(ModifyRestaurantDto modifyRestaurantDto);
    
    Task DeleteRestaurantAsync(Guid id);
}