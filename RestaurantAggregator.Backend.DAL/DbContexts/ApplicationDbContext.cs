using Microsoft.EntityFrameworkCore;
using RestaurantAggregator.DAL.Entities;
using RestaurantAggregator.DAL.Entities.Staff;
using Cook = RestaurantAggregator.DAL.Entities.Staff.Cook;
using Courier = RestaurantAggregator.DAL.Entities.Staff.Courier;
using Manager = RestaurantAggregator.DAL.Entities.Staff.Manager;

namespace RestaurantAggregator.DAL.DbContexts;

public class ApplicationDbContext : DbContext
{
    public DbSet<Dish> Dishes { get; set; }
    
    public DbSet<RestaurantEntity> Restaurants { get; set; }
    
    public DbSet<Menu> Menus { get; set; }
    
    public DbSet<Review> Reviews { get; set; }
    
    public DbSet<CartDish> DishBaskets { get; set; }
    
    public DbSet<Order> Orders { get; set; }
    
    public DbSet<Manager> Managers { get; set; }
    
    public DbSet<Cook> Cooks { get; set; }
    
    public DbSet<Courier> Couriers { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}