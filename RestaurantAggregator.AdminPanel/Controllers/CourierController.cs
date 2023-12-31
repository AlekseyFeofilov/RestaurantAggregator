using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RestaurantAggregator.AdminPanel.Configurations;
using RestaurantAggregator.AdminPanel.Models.Courier;
using RestaurantAggregator.AdminPanel.Models.Restaurant;
using RestaurantAggregator.AdminPanel.Models.Shared;
using RestaurantAggregator.Auth.DAL.IRepositories;
using RestaurantAggregator.Backend.DAL.Entities;
using RestaurantAggregator.Backend.DAL.IRepositories;
using RestaurantAggregator.Common.Dtos.Enums;
using RestaurantAggregator.Common.Extensions;
using ICourierRepository = RestaurantAggregator.Auth.DAL.IRepositories.ICourierRepository;

namespace RestaurantAggregator.AdminPanel.Controllers;

public class CourierController : Controller
{
    private readonly ICourierRepository _authCourierRepository;
    
    private readonly Backend.DAL.IRepositories.ICourierRepository _courierRepository;
    
    private readonly IUserRepository _userRepository;

    private readonly IRestaurantRepository _restaurantRepository;
    
    private readonly IOptions<AppConfigurations> _configurations;

    public CourierController(IUserRepository userRepository, IRestaurantRepository restaurantRepository, ICourierRepository authCourierRepository, Backend.DAL.IRepositories.ICourierRepository courierRepository, IOptions<AppConfigurations> configurations)
    {
        _userRepository = userRepository;
        _restaurantRepository = restaurantRepository;
        _authCourierRepository = authCourierRepository;
        _courierRepository = courierRepository;
        _configurations = configurations;
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
            .GetPagedQueryable(page, _configurations.Value.PageSize);

        var pagedNamedListModel = new PagedNamedListModel(
            contains,
            users.Pagination,
            users.Items.Select(x => new NamedItem(x.Id, x.FullName))
        );
        
        return View("Index", new Tuple<Guid, PagedNamedListModel>(restaurantId, pagedNamedListModel));
    }
    
    public async Task<ActionResult> Search(Guid restaurantId, string contains = "")
    {
        return await Index(restaurantId, contains);
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
        
        await _userRepository.AddRoleAsync(createCourierModel.Id, RoleType.Courier);
        
        return RedirectToAction("Index", new {createCourierModel.RestaurantId});
    }
    
    public ActionResult Edit(Guid id)
    {
        var courier = _courierRepository.FetchDetails(id); 
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
        await _userRepository.RemoveRoleAsync(courierId, RoleType.Courier);
        return RedirectToAction("Index", new {restaurantId});
    }
}