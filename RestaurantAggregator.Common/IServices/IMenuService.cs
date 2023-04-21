using RestaurantAggregator.Common.Models.Dto;

namespace RestaurantAggregator.Common.IServices;

public interface IMenuService
{
    Task<IEnumerable<MenuDto>> FetchMenus(Guid restaurantId, int? page);
}