using Microsoft.EntityFrameworkCore;
using RestaurantAggregator.Backend.DAL.Entities;
using Cook = RestaurantAggregator.Backend.DAL.Entities.Staff.Cook;
using Courier = RestaurantAggregator.Backend.DAL.Entities.Staff.Courier;
using Manager = RestaurantAggregator.Backend.DAL.Entities.Staff.Manager;

namespace RestaurantAggregator.Backend.DAL.DbContexts;

public class ApplicationDbContext : DbContext
{
    public DbSet<Dish> Dishes { get; set; }
    
    public DbSet<Restaurant> Restaurants { get; set; }
    
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
        modelBuilder.Entity<Cook>().HasKey(x => x.Id);
        modelBuilder.Entity<Courier>().HasKey(x => x.Id);
    }
}