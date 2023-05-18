using Microsoft.AspNetCore.Mvc;

namespace RestaurantAggregator.AdminPanel.Controllers;

public class RestaurantStaffController : Controller
{
    public IActionResult ManagerIndex()
    {
        return View();
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