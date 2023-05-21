using RestaurantAggregator.Backend.Common.Dto.Dish;
using RestaurantAggregator.Common.Models;

namespace RestaurantAggregator.Backend.Common.Dto;

public class DishPagedListDto
{
    public IEnumerable<DishDto> Dishes { get; }
    
    public PageInfoModel Pagination { get; }

    public DishPagedListDto(IEnumerable<DishDto> dishes, PageInfoModel pagination)
    {
        Dishes = dishes;
        Pagination = pagination;
    }
}