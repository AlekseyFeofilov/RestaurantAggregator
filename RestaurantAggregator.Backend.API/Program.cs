using Microsoft.AspNetCore.Authentication.JwtBearer;
using RestaurantAggregator.Backend.API.Middleware;
using RestaurantAggregator.BL.Extensions;
using RestaurantAggregator.BL.Helper;
using RestaurantAggregator.Common.Configurations;
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

builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddAutoMapper(typeof(RestaurantAggregator.Backend.API.Helper.MapperProfile));

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