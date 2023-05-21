using Microsoft.AspNetCore.Authentication.JwtBearer;
using RestaurantAggregator.Auth.API.Middleware;
using RestaurantAggregator.Auth.BL.Extensions;
using RestaurantAggregator.Auth.BL.Helpers;
using RestaurantAggregator.Auth.Common.Configuration;
using RestaurantAggregator.Backend.Common.Extensions;
using RestaurantAggregator.Common.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddBL();
builder.Services.AddSwaggerService();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearerAuthenticationScheme();


builder.Services.AddAutoMapper(typeof(MapperProfile));

var app = builder.Build();

app.AddBLPostBuild();
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

Configurations.isDevelopmentEnvironment = app.Environment.IsDevelopment();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();