using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAggregator.AdminPanel.Models;
using RestaurantAggregator.AdminPanel.Models.Restaurant;
using RestaurantAggregator.AdminPanel.Models.Shared;
using RestaurantAggregator.Auth.DAL.Repositories.MangerRepository;
using RestaurantAggregator.Auth.DAL.Repositories.UserRepository;
using RestaurantAggregator.Common.Configurations;
using RestaurantAggregator.Common.Extensions;

namespace RestaurantAggregator.AdminPanel.Controllers;

public class ManagerController : Controller
{
    private readonly DAL.Repositories.ManagerRepository.IManagerRepository _managerRepository;

    private readonly IManagerRepository _authManagerRepository;

    private readonly IUserRepository _userRepository;

    public ManagerController(IManagerRepository authManagerRepository, DAL.Repositories.ManagerRepository.IManagerRepository managerRepository, IUserRepository userRepository)
    {
        _authManagerRepository = authManagerRepository;
        _managerRepository = managerRepository;
        _userRepository = userRepository;
    }

    public async Task<ActionResult> Index(Guid restaurantId, string contains = "", int page = 1)
    {
        var managers = await _managerRepository
            .FetchAllElements()
            .Where(x => x.Restaurant.Id == restaurantId)
            .ToListAsync();

        var users = _userRepository
            .FetchAllElements()
            .Where(x => managers.Select(manager => manager.Id).Contains(x.Id) && x.FullName.Contains(contains ?? ""))
            .GetPagedQueryable(page, AppConfigurations.PageSize);

        var pagedNamedListModel = new PagedNamedListModel(
            contains,
            users.Pagination,
            users.Items.Select(x => new NamedItem(x.Id, x.FullName))
        );
        
        return View("Index", new Tuple<Guid, PagedNamedListModel>(restaurantId, pagedNamedListModel));
    }
    
    public async Task<ActionResult> Search(Guid restaurantId, string contains = "") //todo make normalized name in database
    {
        return await Index(restaurantId, contains);
    }
    
    // public ActionResult Create()
    // {
    //     return View();
    // }
    //
    // [HttpPost]
    // [ValidateAntiForgeryToken]
    // public async Task<ActionResult> Create(CreateRestaurantModel createRestaurantModel)
    // {
    //     //
    //     // var authManager = new Manager()
    //     // await _managerRepository.CreateAsync();
    //     // await _authManagerRepository.CreateAsync();
    //     await _restaurantService.CreateRestaurantAsync(_mapper.Map<CreateRestaurantDto>(createRestaurantModel));
    //     return await Index(createRestaurantModel.Name);
    // }
    //
    // public async Task<ActionResult> Edit(Guid id)
    // {
    //     var restaurant = await _restaurantService.FetchRestaurantDetailsAsync(id);
    //     return View(_mapper.Map<RestaurantModel>(restaurant));
    // }
    //
    // [HttpPost]
    // [ValidateAntiForgeryToken]
    // public async Task<ActionResult> Edit(ModifyRestaurantModel modifyRestaurantModel)
    // {
    //     await _restaurantService.ModifyRestaurantAsync(_mapper.Map<ModifyRestaurantDto>(modifyRestaurantModel));
    //     return await Edit(modifyRestaurantModel.Id);
    // }
    //
    // [HttpPost]
    // [ValidateAntiForgeryToken]
    // public async Task<ActionResult> Delete(Guid restaurantId, Guid managerId)
    // {
    //     await _managerRepository.DeleteAsync(managerId);
    //     await _authManagerRepository.DeleteAsync(managerId);
    //     return await Index(restaurantId);
    // }
}