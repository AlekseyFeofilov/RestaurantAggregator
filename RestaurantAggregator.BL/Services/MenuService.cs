using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAggregator.Common.IServices;
using RestaurantAggregator.Common.Models.Dto;
using RestaurantAggregator.DAL.DbContexts;

namespace RestaurantAggregator.BL.Services;

public class MenuService : IMenuService
{
    private readonly ApplicationDbContext _context;

    private readonly IMapper _mapper;

    public MenuService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MenuDto>> FetchMenus(Guid restaurantId, int? page)
    {
        var menus = await _context.Menus
            .Where(menu => restaurantId == menu.Restaurant.Id)
            .ToListAsync();
        
        return menus.Select(menu => _mapper.Map<MenuDto>(menu));
    }
}