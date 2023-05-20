using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAggregator.AdminPanel.Models;
using RestaurantAggregator.AdminPanel.Models.Manager;
using RestaurantAggregator.AdminPanel.Models.Restaurant;
using RestaurantAggregator.AdminPanel.Models.Shared;
using RestaurantAggregator.Auth.DAL.Repositories.MangerRepository;
using RestaurantAggregator.Auth.DAL.Repositories.UserRepository;
using RestaurantAggregator.Common.Configurations;
using RestaurantAggregator.Common.Extensions;
using RestaurantAggregator.DAL.Entities.Staff;
using RestaurantAggregator.DAL.Repositories.RestaurantRepository;

namespace RestaurantAggregator.AdminPanel.Controllers;

public class ManagerController : Controller
{
    private readonly DAL.Repositories.ManagerRepository.IManagerRepository _managerRepository;

    private readonly IManagerRepository _authManagerRepository;

    private readonly IUserRepository _userRepository;

    private readonly IRestaurantRepository _restaurantRepository;

    public ManagerController(IManagerRepository authManagerRepository, DAL.Repositories.ManagerRepository.IManagerRepository managerRepository, IUserRepository userRepository, IRestaurantRepository restaurantRepository)
    {
        _authManagerRepository = authManagerRepository;
        _managerRepository = managerRepository;
        _userRepository = userRepository;
        _restaurantRepository = restaurantRepository;
    }

    public async Task<ActionResult> Index(Guid restaurantId, string contains = "", int page = 1)
    {
        var managers = await _managerRepository
            .FetchAllElements()
            .Where(x => x.Restaurant.Id == restaurantId)
            .ToListAsync();

        var users = _userRepository
            .FetchAllUsers()
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
    
    public ActionResult Create()
    {
        return View("../Home/Index");
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(CreateManagerModel createManagerModel)
    {
        var restaurant = await _restaurantRepository.FetchRestaurantAsync(createManagerModel.RestaurantId);
        var user = _userRepository.FetchUserDetails(createManagerModel.Id);

        await _authManagerRepository.CreateAsync(new Auth.DAL.Entities.Users.Manager
        {
            Id = user.Id,
            User = user
        });
        
        await _managerRepository.CreateAsync(new Manager
        {
            Id = user.Id,
            Restaurant = restaurant
        });
        
        return await Index(createManagerModel.RestaurantId);
    }
    
    public ActionResult Edit(Guid id)
    {
        var manager = _managerRepository.FetchDetails(id); //crutch нельзя получить ресторана( 
        var user = _userRepository.FetchUserDetails(id);
        
        
        return View(new ManagerModel
        {
            Id = id,
            Name = $"{user.FullName} ({user.Email})",
            Restaurant = new RestaurantModel
            {
                Id = manager.Restaurant.Id,
                Name = manager.Restaurant.Name
            } 
        });
    }
    
    // [HttpPost]
    // [ValidateAntiForgeryToken]
    // public async Task<ActionResult> Edit(ModifyRestaurantModel modifyRestaurantModel)
    // {
    //     await _restaurantService.ModifyRestaurantAsync(_mapper.Map<ModifyRestaurantDto>(modifyRestaurantModel));
    //     return await Edit(modifyRestaurantModel.Id);
    // }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Delete(Guid restaurantId, Guid managerId)
    {
        await _managerRepository.DeleteAsync(managerId);
        await _authManagerRepository.DeleteAsync(managerId);
        return RedirectToAction("Index", new {restaurantId}); //todo make this way any time that i actually redirect
    }
}