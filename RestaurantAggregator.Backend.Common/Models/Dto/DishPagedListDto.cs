using RestaurantAggregator.Common.Models.Dto.Dish;

namespace RestaurantAggregator.Common.Models.Dto;

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