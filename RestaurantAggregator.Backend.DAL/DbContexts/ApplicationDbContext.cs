using Microsoft.EntityFrameworkCore;
using RestaurantAggregator.DAL.Entities;
using RestaurantAggregator.DAL.Entities.Staff;

namespace RestaurantAggregator.DAL.DbContexts;

public class ApplicationDbContext : DbContext
{
    public DbSet<Dish> Dishes { get; set; }
    
    public DbSet<RestaurantEntity> Restaurants { get; set; }
    
    public DbSet<Menu> Menus { get; set; }
    
    public DbSet<Review> Reviews { get; set; }
    
    public DbSet<CartDish> DishBaskets { get; set; }
    
    public DbSet<Order> Orders { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}