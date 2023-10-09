using MagazynEdu.DataAccess;
using Microsoft.OpenApi.Models;
using NLog.Web;
using Microsoft.EntityFrameworkCore;
using MagazynEdu.ApplicationsServices.API.Domain;
using MediatR;
using MagazynEdu.ApplicationsServices.Mappings;
using MagazynEdu.DataAccess.CQRS;

var builder = WebApplication.CreateBuilder(args);

//NLog: Setup NLog for Dependency Injection

builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();

//configure service

builder.Services.AddTransient<IQueryExecutor, QueryExecutor>();
builder.Services.AddTransient<ICommandExecutor, CommandExecutor>();

builder.Services.AddAutoMapper(typeof(DevicesProfile));

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(typeof(ResponseBase<>).Assembly);
});

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddDbContext<WarehouseStorageContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("WarehouseDatabaseConnection")));

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MagazynEdu", Version = "v1" });
});

var app = builder.Build();

//configure

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MagazynEdu"));
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();