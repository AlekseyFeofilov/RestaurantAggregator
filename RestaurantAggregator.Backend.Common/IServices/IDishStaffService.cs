using System.Security.Claims;
using RestaurantAggregator.Backend.Common.Dto;

namespace RestaurantAggregator.Backend.Common.IServices;

public interface IDishStaffService
{
    Task<DishPagedListDto> FetchManagerAllDishes(ClaimsPrincipal claimsPrincipal, DishStaffOptionsDto dishStaffOptionsDto);
    
    Task<DishPagedListDto> FetchCookAllDishes(ClaimsPrincipal claimsPrincipal, DishStaffOptionsDto dishStaffOptionsDto);
    
    Task<DishPagedListDto> FetchCourierAllDishes(ClaimsPrincipal claimsPrincipal, DishStaffOptionsDto dishStaffOptionsDto);
}