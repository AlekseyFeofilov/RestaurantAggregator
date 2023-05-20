using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RestaurantAggregator.Auth.Common.Models.Enums;
using RestaurantAggregator.Auth.DAL.Entities.IdentityEntities;
using RestaurantAggregator.Auth.DAL.Entities.Users;

namespace RestaurantAggregator.Auth.DAL.DbContexts;

public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
{
    public DbSet<User> Users { get; set; }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<Cook> Cooks { get; set; }

    public DbSet<Courier> Couriers { get; set; }

    public DbSet<Manager> Managers { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    //todo: добавить has data для ролей
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Manager>().HasKey(x => x.Id);
        modelBuilder.Entity<Cook>().HasKey(x => x.Id);
        modelBuilder.Entity<Courier>().HasKey(x => x.Id);
        
        modelBuilder.Entity<User>()
            .HasOne<Customer>()
            .WithOne(x => x.User)
            .HasForeignKey<Customer>().IsRequired();

        modelBuilder.Entity<User>()
            .HasOne<Manager>()
            .WithOne(x => x.User)
            .HasForeignKey<Manager>();

        modelBuilder.Entity<User>()
            .HasOne<Cook>()
            .WithOne(x => x.User)
            .HasForeignKey<Cook>();

        modelBuilder.Entity<User>()
            .HasOne<Courier>()
            .WithOne(x => x.User)
            .HasForeignKey<Courier>();

        // crutch: сделать через static класс
        modelBuilder.Entity<Role>().HasData(new Role[]
        {
            new()
            {
                Id = Guid.Parse("93eb91dd-e76a-45d9-acc8-6c519e742f26"),
                Name = "Customer",
                NormalizedName = "CUSTOMER",
                Type = RoleType.Customer
            },
            new()
            {
                Id = Guid.Parse("7ab6e5fd-8008-47d1-879f-3e197f67670c"),
                Name = "Manager",
                NormalizedName = "MANAGER",
                Type = RoleType.Manager
            },
            new()
            {
                Id = Guid.Parse("dcbbcfd3-554f-4574-a9c0-dee956c5ef2a"),
                Name = "Courier",
                NormalizedName = "COURIER",
                Type = RoleType.Courier
            },
            new()
            {
                Id = Guid.Parse("4482297d-6e7e-40d7-9ad1-78dffd3942f0"),
                Name = "Cook",
                NormalizedName = "COOK",
                Type = RoleType.Cook
            },
        });

        // todo: remove this example and make real claims
        modelBuilder.Entity<IdentityRoleClaim<Guid>>().HasData(new IdentityRoleClaim<Guid>[]
        {
            new()
            {
                Id = 1,
                RoleId = Guid.Parse("93eb91dd-e76a-45d9-acc8-6c519e742f26"),
                ClaimType = "FirstTestType"
            },
            new()
            {
                Id = 2,
                RoleId = Guid.Parse("93eb91dd-e76a-45d9-acc8-6c519e742f26"),
                ClaimType = "SecondTestType"
            }
        });
    }
}