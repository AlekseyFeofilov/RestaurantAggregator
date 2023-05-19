using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAggregator.AdminPanel.Models;
using RestaurantAggregator.Auth.DAL.Entities.Users;
using RestaurantAggregator.Auth.DAL.Repositories.MangerRepository;
using RestaurantAggregator.Common.Configurations;
using RestaurantAggregator.Common.Extensions;
using RestaurantAggregator.DAL.DbContexts;
using RestaurantAggregator.DAL.Repositories.ManagerRepository;
using ApplicationDbContext = RestaurantAggregator.Auth.DAL.DbContexts.ApplicationDbContext;
using IManagerRepository = RestaurantAggregator.Auth.DAL.Repositories.MangerRepository.IManagerRepository;

namespace RestaurantAggregator.AdminPanel.Controllers;

public class RestaurantStaffController : Controller
{
    

    private readonly ApplicationDbContext _context;

    public RestaurantStaffController(DAL.Repositories.ManagerRepository.IManagerRepository managerRepository, ApplicationDbContext context, IManagerRepository authManagerRepository)
    {
        _context = context;
    }

    public IActionResult CookIndex()
    {
        return View();
    }

    public IActionResult CourierIndex()
    {
        return View();
    }
}