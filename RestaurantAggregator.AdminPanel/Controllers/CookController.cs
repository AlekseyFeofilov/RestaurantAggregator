using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RestaurantAggregator.AdminPanel.Configurations;
using RestaurantAggregator.AdminPanel.Models.Cook;
using RestaurantAggregator.AdminPanel.Models.Restaurant;
using RestaurantAggregator.AdminPanel.Models.Shared;
using RestaurantAggregator.Auth.DAL.IRepositories;
using RestaurantAggregator.Backend.DAL.Entities;
using RestaurantAggregator.Backend.DAL.IRepositories;
using RestaurantAggregator.Common.Dtos.Enums;
using RestaurantAggregator.Common.Extensions;
using ICookRepository = RestaurantAggregator.Auth.DAL.IRepositories.ICookRepository;

namespace RestaurantAggregator.AdminPanel.Controllers;

public class CookController : Controller
{
    private readonly ICookRepository _authCookRepository;
    
    private readonly Backend.DAL.IRepositories.ICookRepository _cookRepository;

    private readonly IUserRepository _userRepository;
    
    private readonly IRestaurantRepository _restaurantRepository;
    
    private readonly IOptions<AppConfigurations> _configurations;

    public CookController(Backend.DAL.IRepositories.ICookRepository cookRepository, IUserRepository userRepository, ICookRepository authCookRepository, IRestaurantRepository restaurantRepository, IOptions<AppConfigurations> configurations)
    {
        _cookRepository = cookRepository;
        _userRepository = userRepository;
        _authCookRepository = authCookRepository;
        _restaurantRepository = restaurantRepository;
        _configurations = configurations;
    }

    public async Task<ActionResult> Index(Guid restaurantId, string contains = "", int page = 1)
    {
        var cooks = await _cookRepository
            .FetchAllElements()
            .Where(x => x.Restaurant.Id == restaurantId)
            .ToListAsync();

        var users = _userRepository
            .FetchAllUsers()
            .Where(x => cooks.Select(cook => cook.Id).Contains(x.Id) && x.FullName.Contains(contains ?? ""))
            .GetPagedQueryable(page, _configurations.Value.PageSize);

        var pagedNamedListModel = new PagedNamedListModel(
            contains,
            users.Pagination,
            users.Items.Select(x => new NamedItem(x.Id, x.FullName))
        );
        
        return View("Index", new Tuple<Guid, PagedNamedListModel>(restaurantId, pagedNamedListModel));
    }
    
    public async Task<ActionResult> Search(Guid restaurantId, string contains = "") //todo нормализовать всё, по чему происходит поиск в бд
    {
        return await Index(restaurantId, contains);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(CreateCookModel createCookModel)
    {
        var user = _userRepository.FetchUserDetails(createCookModel.Id);
        var restaurant = await _restaurantRepository.FetchRestaurantAsync(createCookModel.RestaurantId);

        await _authCookRepository.CreateAsync(new Auth.DAL.Entities.Users.Cook
        {
            Id = user.Id,
            User = user
        });
        
        await _cookRepository.CreateAsync(new Cook
        {
            Id = user.Id,
            Restaurant = restaurant
        });
        
        await _userRepository.AddRoleAsync(createCookModel.Id, RoleType.Cook);
        
        return RedirectToAction("Index", new {createCookModel.RestaurantId});
    }
    
    public ActionResult Edit(Guid id)
    {
        var cook = _cookRepository.FetchDetails(id); 
        var user = _userRepository.FetchUserDetails(id);
        
        return View(new CookModel
        {
            Id = id,
            Name = $"{user.FullName} ({user.Email})",
            Restaurant = new RestaurantModel
            {
                Id = cook.Restaurant.Id,
                Name = cook.Restaurant.Name
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
    public async Task<ActionResult> Delete(Guid restaurantId, Guid cookId)
    {
        await _cookRepository.DeleteAsync(cookId);
        await _authCookRepository.DeleteAsync(cookId);
        await _userRepository.RemoveRoleAsync(cookId, RoleType.Cook);
        return RedirectToAction("Index", new {restaurantId});
    }
}