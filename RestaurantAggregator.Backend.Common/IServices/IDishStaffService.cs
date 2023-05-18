using System.Security.Claims;
using RestaurantAggregator.Common.Models.Dto;

namespace RestaurantAggregator.Common.IServices;

public interface IDishStaffService
{
    Task<DishPagedListDto> FetchManagerAllDishes(ClaimsPrincipal claimsPrincipal, DishStaffOptionsDto dishStaffOptionsDto);
    
    Task<DishPagedListDto> FetchCookAllDishes(ClaimsPrincipal claimsPrincipal, DishStaffOptionsDto dishStaffOptionsDto);
    
    Task<DishPagedListDto> FetchCourierAllDishes(ClaimsPrincipal claimsPrincipal, DishStaffOptionsDto dishStaffOptionsDto);
}