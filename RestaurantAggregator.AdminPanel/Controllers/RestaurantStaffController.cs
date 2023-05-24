using Microsoft.AspNetCore.Mvc;
using ApplicationDbContext = RestaurantAggregator.Auth.DAL.DbContexts.ApplicationDbContext;
using IManagerRepository = RestaurantAggregator.Auth.DAL.IRepositories.IManagerRepository;

namespace RestaurantAggregator.AdminPanel.Controllers;

public class RestaurantStaffController : Controller
{
    

    private readonly ApplicationDbContext _context;

    public RestaurantStaffController(Backend.DAL.IRepositories.IManagerRepository managerRepository, ApplicationDbContext context, IManagerRepository authManagerRepository)
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