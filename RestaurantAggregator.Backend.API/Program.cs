using Microsoft.AspNetCore.Authentication.JwtBearer;
using RestaurantAggregator.Backend.API.Extensions;
using RestaurantAggregator.Backend.API.Middleware;
using RestaurantAggregator.Backend.BL.Extensions;
using RestaurantAggregator.Backend.Common.Configurations;
using RestaurantAggregator.Backend.Common.Extensions;
using RestaurantAggregator.Backend.API.Extensions;
using RestaurantAggregator.Common.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddBL();
builder.Services.AddSwaggerService();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearerAuthenticationScheme();

builder.Services.AddAutoMapper();
builder.Services.AddAuthorizationPolicies();
builder.Services.AddAuthorizationHandlers();
builder.Services.AddAuthorizationPolicyProvider();

var app = builder.Build();

app.AddBLPostBuild();
app.UseMiddleware<ErrorHandlingMiddleware>();

AppConfigurations.isDevelopmentEnvironment = app.Environment.IsDevelopment();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run(); //todo patch как очистить, сделать общий коммон для всего солюшена