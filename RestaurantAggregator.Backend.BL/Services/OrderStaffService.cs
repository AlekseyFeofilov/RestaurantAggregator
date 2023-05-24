using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RestaurantAggregator.Backend.Common.Configurations;
using RestaurantAggregator.Backend.Common.Dtos.Order;
using RestaurantAggregator.Backend.Common.IServices;
using RestaurantAggregator.Backend.DAL.DbContexts;
using RestaurantAggregator.Backend.DAL.Entities;
using RestaurantAggregator.Backend.DAL.IRepositories;
using RestaurantAggregator.Common.Dtos;
using RestaurantAggregator.Common.Dtos.Enums;
using RestaurantAggregator.Common.Extensions;

namespace RestaurantAggregator.Backend.BL.Services;

public class OrderStaffService : IOrderStaffService
{
    private readonly ApplicationDbContext _context;

    private readonly IManagerRepository _managerRepository;

    private readonly IOptions<AppConfigurations> _configurations;

    public OrderStaffService(ApplicationDbContext context, IManagerRepository managerRepository, IOptions<AppConfigurations> configurations)
    {
        _context = context;
        _managerRepository = managerRepository;
        _configurations = configurations;
    }

    public async Task<PagedEnumerable<OrderInfoDto>> FetchAllCookOrdersAsync(ClaimsPrincipal claimsPrincipal, OrderOptions orderOptions)
    {
        var cookId = claimsPrincipal.GetNameIdentifier();
        
        var pagedQueryableOrders = GetOrderWithOptions(orderOptions)
            .Where(x => x.Cook != null && x.Cook.Id == cookId || x.Status == OrderStatus.Created)
            .Select(x => new OrderInfoDto(x.Id, x.DeliveryTime, x.OrderTime, x.Status, x.Price, x.Number))
            .GetPagedQueryable(orderOptions.Page, _configurations.Value.PageSize);
        
        return new PagedEnumerable<OrderInfoDto>(await pagedQueryableOrders.Items.ToListAsync(), pagedQueryableOrders.Pagination);
    }

    public async Task<PagedEnumerable<OrderInfoDto>> FetchAllManagerOrdersAsync(ClaimsPrincipal claimsPrincipal, OrderOptions orderOptions)
    {
        var restaurantId = _managerRepository.FetchDetails(claimsPrincipal.GetNameIdentifier()).Restaurant.Id;

        var pagedQueryableOrders = GetOrderWithOptions(orderOptions)
            .Where(x => x.Restaurant.Id == restaurantId)
            .Select(x => new OrderInfoDto(x.Id, x.DeliveryTime, x.OrderTime, x.Status, x.Price, x.Number))
            .GetPagedQueryable(orderOptions.Page, _configurations.Value.PageSize);
        
        return new PagedEnumerable<OrderInfoDto>(await pagedQueryableOrders.Items.ToListAsync(), pagedQueryableOrders.Pagination);

    }

    public async Task<PagedEnumerable<OrderInfoDto>> FetchAllCourierOrdersAsync(ClaimsPrincipal claimsPrincipal, OrderOptions orderOptions)
    {
        var courierId = claimsPrincipal.GetNameIdentifier();
        
        var pagedQueryableOrders = GetOrderWithOptions(orderOptions)
            .Where(x => x.Courier != null && x.Courier.Id == courierId)
            .Select(x => new OrderInfoDto(x.Id, x.DeliveryTime, x.OrderTime, x.Status, x.Price, x.Number))
            .GetPagedQueryable(orderOptions.Page, _configurations.Value.PageSize);
        
        return new PagedEnumerable<OrderInfoDto>(await pagedQueryableOrders.Items.ToListAsync(), pagedQueryableOrders.Pagination);
    }

    private IQueryable<Order> GetOrderWithOptions(OrderOptions orderOptions)
    {
        var orders = _context.Orders.AsQueryable();

        if (orderOptions.Current)
        {
            orders = GetActiveOrders(orders);
        }

        if (orderOptions.NumberContains != null)
        {
            orders = GetOrderWithNumber(orders, orderOptions.NumberContains);
        }

        if (orderOptions.StartDate != null)
        {
            orders = GetOrderBeforeDate(orders, (DateTime)orderOptions.StartDate);
        }
        
        if (orderOptions.EndDate != null)
        {
            orders = GetOrderUntilDate(orders, (DateTime)orderOptions.EndDate);
        }

        return orders;
    }

    private IQueryable<Order> GetActiveOrders(IQueryable<Order> queryable)
    {
        return queryable.Where(x => x.Status != OrderStatus.Delivered);
    }

    private IQueryable<Order> GetOrderUntilDate(IQueryable<Order> queryable, DateTime dateTime)
    {
        return queryable.Where(x => x.OrderTime < dateTime);
    }

    private IQueryable<Order> GetOrderBeforeDate(IQueryable<Order> queryable, DateTime dateTime)
    {
        return queryable.Where(x => x.OrderTime > dateTime);
    }

    private IQueryable<Order> GetOrderWithNumber(IQueryable<Order> queryable, string contains)
    {
        return queryable.Where(x => x.Number.Contains(contains));
    }
}