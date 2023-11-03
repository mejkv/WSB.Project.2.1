using AirShop.WebApiPostgre.Data.Profiles;
using AirShop.WebApiPostgre.Data.ShopDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

// NLog: Setup NLog for Dependency Injection
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();

//logs

//configure AutoMapper
builder.Services.AddAutoMapper(typeof(ProductsProfile));

// configure service

builder.Services.AddDbContext<ShopDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DatabaseConnection")));

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Project.PO.Shop", Version = "v1" });
});

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
app.Use(async (context, next) =>
{
    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
    logger.LogInformation($"{DateTime.Now}: API started.");

    try
    {
        var dbContext = context.RequestServices.GetRequiredService<ShopDbContext>();
        await dbContext.Database.MigrateAsync();
        logger.LogInformation("Database connection is successful.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Error connecting to the database.");
    }

    logger.LogInformation($"{DateTime.Now}: Request: {context.Request.Method} {context.Request.Path}");

    var originalBodyStream = context.Response.Body;
    using (var responseBody = new MemoryStream())
    {
        context.Response.Body = responseBody;

        await next();

        responseBody.Seek(0, SeekOrigin.Begin);
        var responseBodyText = new StreamReader(responseBody).ReadToEnd();
        logger.LogInformation($"{DateTime.Now}: Response: {context.Response.StatusCode} {responseBodyText}");

        responseBody.Seek(0, SeekOrigin.Begin);
        await responseBody.CopyToAsync(originalBodyStream);
    }
});

app.Use(async (context, next) =>
{
    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();

    logger.LogInformation($"{DateTime.Now}: Request: {context.Request.Method} {context.Request.Path}");

    var originalBodyStream = context.Response.Body;
    using (var responseBody = new MemoryStream())
    {
        context.Response.Body = responseBody;

        await next();

        responseBody.Seek(0, SeekOrigin.Begin);
        var responseBodyText = new StreamReader(responseBody).ReadToEnd();
        logger.LogInformation($"{DateTime.Now}: Response: {context.Response.StatusCode} {responseBodyText}");

        responseBody.Seek(0, SeekOrigin.Begin);
        await responseBody.CopyToAsync(originalBodyStream);
    }
});


/*app.(async context =>
{
    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
    logger.LogInformation($"\n{DateTime.Now}: API started.");

    try
    {
        var dbContext = context.RequestServices.GetRequiredService<ShopDbContext>();
        await dbContext.Database.MigrateAsync();
        logger.LogInformation("\nDatabase connection is successful.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Error connecting to the database.");
    }
});*/
app.MapControllers();

app.Run();