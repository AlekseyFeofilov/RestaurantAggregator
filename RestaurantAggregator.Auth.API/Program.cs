using Microsoft.AspNetCore.Authentication.JwtBearer;
using RestaurantAggregator.Auth.API.MapperProfiles;
using RestaurantAggregator.Auth.API.Middleware;
using RestaurantAggregator.Auth.BL.Extensions;
using RestaurantAggregator.Auth.BL.MapperProfiles;
using RestaurantAggregator.Auth.Common.Configurations;
using RestaurantAggregator.Backend.Common.Extensions;
using RestaurantAggregator.Common.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.Configure<AppConfigurations>(builder.Configuration.GetSection("AppConfigurations"));
builder.Services.Configure<JwtConfigurations>(builder.Configuration.GetSection("JwtConfigurations"));

builder.Services.AddBL(); //todo убраться в Program
builder.Services.AddSwaggerService();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearerAuthenticationScheme(builder.Configuration.GetSection("JwtConfigurations").Get<JwtConfigurations>());


builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddAutoMapper(typeof(AccountMapperProfile));

var app = builder.Build();

app.AddBLPostBuild();
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();