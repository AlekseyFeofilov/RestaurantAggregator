using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAggregator.AdminPanel.Models.Cook;
using RestaurantAggregator.AdminPanel.Models.Manager;
using RestaurantAggregator.AdminPanel.Models.Restaurant;
using RestaurantAggregator.AdminPanel.Models.Shared;
using RestaurantAggregator.Auth.DAL.Repositories.UserRepository;
using RestaurantAggregator.Common.Configurations;
using RestaurantAggregator.Common.Extensions;
using RestaurantAggregator.DAL.Entities.Staff;
using RestaurantAggregator.DAL.Repositories.CookRepository;
using RestaurantAggregator.DAL.Repositories.RestaurantRepository;

namespace RestaurantAggregator.AdminPanel.Controllers;

public class CookController : Controller
{
    private readonly Auth.DAL.Repositories.CookRepository.ICookRepository _authCookRepository;
    
    private readonly ICookRepository _cookRepository;

    private readonly IUserRepository _userRepository;
    
    private readonly IRestaurantRepository _restaurantRepository;

    public CookController(ICookRepository cookRepository, IUserRepository userRepository, Auth.DAL.Repositories.CookRepository.ICookRepository authCookRepository, IRestaurantRepository restaurantRepository)
    {
        _cookRepository = cookRepository;
        _userRepository = userRepository;
        _authCookRepository = authCookRepository;
        _restaurantRepository = restaurantRepository;
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
        
        return await Index(createCookModel.RestaurantId);
    }
    
    public ActionResult Edit(Guid id)
    {
        var cook = _cookRepository.FetchDetails(id); //crutch нельзя получить ресторана( 
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
        return RedirectToAction("Index", new {restaurantId}); //todo make this way any time that i actually redirect
    }
}