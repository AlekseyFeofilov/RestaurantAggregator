using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAggregator.AdminPanel.Models.Courier;
using RestaurantAggregator.AdminPanel.Models.Manager;
using RestaurantAggregator.AdminPanel.Models.Restaurant;
using RestaurantAggregator.AdminPanel.Models.Shared;
using RestaurantAggregator.Auth.DAL.Repositories.UserRepository;
using RestaurantAggregator.Backend.Common.Configurations;
using RestaurantAggregator.Backend.DAL.Entities.Staff;
using RestaurantAggregator.Backend.DAL.Repositories.CourierRepository;
using RestaurantAggregator.Backend.DAL.Repositories.RestaurantRepository;
using RestaurantAggregator.Common.Extensions;

namespace RestaurantAggregator.AdminPanel.Controllers;

public class CourierController : Controller
{
    private readonly Auth.DAL.Repositories.CourierRepository.ICourierRepository _authCourierRepository;
    
    private readonly ICourierRepository _courierRepository;
    
    private readonly IUserRepository _userRepository;

    private readonly IRestaurantRepository _restaurantRepository;

    public CourierController(IUserRepository userRepository, IRestaurantRepository restaurantRepository, Auth.DAL.Repositories.CourierRepository.ICourierRepository authCourierRepository, ICourierRepository courierRepository)
    {
        _userRepository = userRepository;
        _restaurantRepository = restaurantRepository;
        _authCourierRepository = authCourierRepository;
        _courierRepository = courierRepository;
    }

    public async Task<ActionResult> Index(Guid restaurantId, string contains = "", int page = 1)
    {
        var couriers = await _courierRepository
            .FetchAllElements()
            .Where(x => x.Restaurant.Id == restaurantId)
            .ToListAsync();

        var users = _userRepository
            .FetchAllUsers()
            .Where(x => couriers.Select(courier => courier.Id).Contains(x.Id) && x.FullName.Contains(contains ?? ""))
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
    public async Task<ActionResult> Create(CreateCourierModel createCourierModel)
    {
        var restaurant = await _restaurantRepository.FetchRestaurantAsync(createCourierModel.RestaurantId);
        var user = _userRepository.FetchUserDetails(createCourierModel.Id);

        await _authCourierRepository.CreateAsync(new Auth.DAL.Entities.Users.Courier
        {
            Id = user.Id,
            User = user
        });
        
        await _courierRepository.CreateAsync(new Courier
        {
            Id = user.Id,
            Restaurant = restaurant
        });
        
        return await Index(createCourierModel.RestaurantId);
    }
    
    public ActionResult Edit(Guid id)
    {
        var courier = _courierRepository.FetchDetails(id); //crutch нельзя получить ресторана( 
        var user = _userRepository.FetchUserDetails(id);
        
        
        return View(new CourierModel
        {
            Id = id,
            Name = $"{user.FullName} ({user.Email})",
            Restaurant = new RestaurantModel
            {
                Id = courier.Restaurant.Id,
                Name = courier.Restaurant.Name
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
    public async Task<ActionResult> Delete(Guid restaurantId, Guid courierId)
    {
        await _courierRepository.DeleteAsync(courierId);
        await _authCourierRepository.DeleteAsync(courierId);
        return RedirectToAction("Index", new {restaurantId}); //todo make this way any time that i actually redirect
    }
}