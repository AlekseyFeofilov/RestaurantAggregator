using RestaurantAggregator.Common.Dtos;

namespace RestaurantAggregator.Backend.Common.Dtos.Dish;

public class DishPagedListDto
{
    public IEnumerable<DishDto> Dishes { get; }
    
    public PageInfo Pagination { get; }

    public DishPagedListDto(IEnumerable<DishDto> dishes, PageInfo pagination)
    {
        Dishes = dishes;
        Pagination = pagination;
    }
}