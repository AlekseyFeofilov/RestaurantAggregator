using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RestaurantAggregator.AdminPanel.Configurations;
using RestaurantAggregator.AdminPanel.Models.Shared;
using RestaurantAggregator.Auth.DAL.IRepositories;
using RestaurantAggregator.Common.Extensions;

namespace RestaurantAggregator.AdminPanel.Controllers;

public class UserController : Controller
{
    private readonly IUserRepository _userRepository;

    private readonly IOptions<AppConfigurations> _configurations;

    public UserController(IUserRepository userRepository, IOptions<AppConfigurations> configurations)
    {
        _userRepository = userRepository;
        _configurations = configurations;
    }

    public ActionResult
        Index(string staff, Guid restaurantId, string contains = "",
            int page = 1)
    {
        var users = _userRepository
            .FetchAllNonStaffUsers()
            .Where(x => x.FullName.Contains(contains ?? "") || x.Email.Contains(contains ?? ""))
            .GetPagedQueryable(page, _configurations.Value.PageSize);

        var pagedNamedListModel = new PagedNamedListModel(
            contains,
            users.Pagination,
            users.Items.Select(x => new NamedItem(x.Id, $"{x.FullName} ({x.Email})"))
        );

        return View("Index", new Tuple<PagedNamedListModel, string, Guid>(pagedNamedListModel, staff, restaurantId));
    }

    public ActionResult
        Search(string staff, Guid restaurantId, string contains = "")
    {
        return Index(staff, restaurantId, contains);
    }
}