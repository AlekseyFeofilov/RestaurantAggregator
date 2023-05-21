using RestaurantAggregator.Backend.Common.Dto;

namespace RestaurantAggregator.Backend.Common.IServices;

public interface IMenuService
{
    Task<IEnumerable<MenuDto>> FetchMenus(Guid restaurantId, int? page);
}