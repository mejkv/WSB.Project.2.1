using AirShop.WebApiPostgre.Data.ShopDbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/health")]
public class HealthController : ControllerBase
{
    private readonly ShopDbContext dbContext;

    public HealthController(ShopDbContext dbContext)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    [HttpGet]
    public IActionResult CheckHealth()
    {
        try
        {
            dbContext.Database.OpenConnection();
            dbContext.Database.CloseConnection();

            return Ok(new { Status = "Healthy", Message = "Database connection is successful." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Status = "Unhealthy", Message = $"Error connecting to the database: {ex.Message}" });
        }
    }
}
