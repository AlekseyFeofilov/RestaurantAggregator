
using RestaurantAggregator.Auth.DAL.Extensions;
using RestaurantAggregator.Auth.DAL.Repositories.CookRepository;
using RestaurantAggregator.Auth.DAL.Repositories.CourierRepository;
using RestaurantAggregator.Auth.DAL.Repositories.MangerRepository;
using RestaurantAggregator.Auth.DAL.Repositories.UserRepository;
using RestaurantAggregator.BL.Extensions;
using RestaurantAggregator.BL.Helper;
using RestaurantAggregator.BL.Services;
using RestaurantAggregator.Common.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddDAL();
builder.Services.AddScoped<IManagerRepository, ManagerRepository>();
builder.Services.AddScoped<ICookRepository, CookRepository>();
builder.Services.AddScoped<ICourierRepository, CourierRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddBL();
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddAutoMapper(typeof(RestaurantAggregator.AdminPanel.Helpers.MapperProfile));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();