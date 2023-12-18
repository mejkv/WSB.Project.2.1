using AirShop.WebApiPostgre.ApiServices;
using AirShop.WebApiPostgre.Controllers;
using AirShop.WebApiPostgre.Data.Profiles;
using AirShop.WebApiPostgre.Data.ShopDbContext;
using AirShop.WebApiPostgre.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NLog.Web;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// NLog: Setup NLog for Dependency Injection
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();
string nlogConfigPath = Path.Combine(Directory.GetCurrentDirectory(), "Config", "nlog.config");
NLogBuilder.ConfigureNLog(nlogConfigPath);
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
var logger = NLogBuilder.ConfigureNLog(nlogConfigPath).GetCurrentClassLogger();

//logs

//configure AutoMapper
builder.Services.AddAutoMapper(typeof(ProductsProfile));
builder.Services.AddAutoMapper(typeof(ReceiptProfile));
builder.Services.AddAutoMapper(typeof(UserProfile));

// configure service
logger.Info("Starting services");
builder.Services.AddScoped<IUserService, UserService>();

logger.Info("Creating database connection");
builder.Services.AddDbContext<ShopDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DatabaseConnection")));

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Project.PO.Shop", Version = "v1" });
});

logger.Info("Starting API");
var app = builder.Build();

// configure
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "shop"));
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

// Logging middleware for incoming requests and responses
app.UseMiddleware<LoggingMiddleware>();

//Controllers
app.MapControllers();

logger.Info("API started");
app.Run();