using Microsoft.AspNetCore.Mvc;
using RestaurantAggregator.AdminPanel.Models.Shared;
using RestaurantAggregator.Auth.DAL.IRepositories;
using RestaurantAggregator.Backend.Common.Configurations;
using RestaurantAggregator.Common.Extensions;

namespace RestaurantAggregator.AdminPanel.Controllers;

public class UserController : Controller
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public ActionResult
        Index(string staff, Guid restaurantId, string contains = "",
            int page = 1) //todo высвечивать только тех, кто ещё не стафф или не добавлять стаффа, если он уже есть
    {
        var users = _userRepository
            .FetchAllNonStaffUsers()
            .Where(x => x.FullName.Contains(contains ?? "") || x.Email.Contains(contains ?? ""))
            .GetPagedQueryable(page, AppConfigurations.PageSize);

        var pagedNamedListModel = new PagedNamedListModel(
            contains,
            users.Pagination,
            users.Items.Select(x => new NamedItem(x.Id, $"{x.FullName} ({x.Email})"))
        );

        return View("Index", new Tuple<PagedNamedListModel, string, Guid>(pagedNamedListModel, staff, restaurantId));
    }

    public ActionResult
        Search(string staff, Guid restaurantId, string contains = "") //todo make normalized name in database
    {
        return Index(staff, restaurantId, contains);
    }
}