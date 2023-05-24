using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using RestaurantAggregator.Backend.Common.Dto;
using RestaurantAggregator.Backend.Common.IServices;
using RestaurantAggregator.Backend.DAL.DbContexts;
using RestaurantAggregator.Backend.DAL.Entities;
using RestaurantAggregator.Backend.DAL.Repositories.ManagerRepository;
using RestaurantAggregator.Common.Extensions;
using RestaurantAggregator.Common.Models.Enums;

namespace RestaurantAggregator.Backend.BL.Services;

public class OrderStaffService : IOrderStaffService
{
    private readonly ApplicationDbContext _context;

    private readonly IManagerRepository _managerRepository;

    public OrderStaffService(ApplicationDbContext context, IManagerRepository managerRepository)
    {
        _context = context;
        _managerRepository = managerRepository;
    }

    public Task<List<OrderInfoDto>> FetchAllCookOrdersAsync(ClaimsPrincipal claimsPrincipal, OrderOptions orderOptions)
    {
        var cookId = claimsPrincipal.GetNameIdentifier();
        
        return GetOrderWithOptions(orderOptions)
            .Where(x => x.Cook != null && x.Cook.Id == cookId || x.Status == OrderStatus.Created)
            .Select(x => new OrderInfoDto(x.Id, x.DeliveryTime, x.OrderTime, x.Status, x.Price, x.Number)) //todo make mapping with automapper
            .ToListAsync();
    }

    public Task<List<OrderInfoDto>> FetchAllManagerOrdersAsync(ClaimsPrincipal claimsPrincipal, OrderOptions orderOptions)
    {
        var restaurantId = _managerRepository.FetchDetails(claimsPrincipal.GetNameIdentifier()).Restaurant.Id;

        return GetOrderWithOptions(orderOptions)
            .Where(x => x.Restaurant.Id == restaurantId)
            .Select(x => new OrderInfoDto(x.Id, x.DeliveryTime, x.OrderTime, x.Status, x.Price, x.Number))
            .ToListAsync();
    }

    public Task<List<OrderInfoDto>> FetchAllCourierOrdersAsync(ClaimsPrincipal claimsPrincipal, OrderOptions orderOptions)
    {
        var courierId = claimsPrincipal.GetNameIdentifier();
        
        return GetOrderWithOptions(orderOptions)
            .Where(x => x.Courier != null && x.Courier.Id == courierId)
            .Select(x => new OrderInfoDto(x.Id, x.DeliveryTime, x.OrderTime, x.Status, x.Price, x.Number))
            .ToListAsync();
    }

    private IQueryable<Order> GetOrderWithOptions(OrderOptions orderOptions) //todo нет пагинации
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