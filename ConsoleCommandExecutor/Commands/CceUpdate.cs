using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using AirShop.DataAccess;
using AirShop.DataAccess.Data.ShopDbContext;
using ConsoleCommandExecutor;

namespace CommandExecutorConsole
{
    public class CceUpdate : CommandBase
    {
        private readonly IConfiguration _configuration;

        public CceUpdate(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override void Execute()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Start command CCE.Update");
            Console.ResetColor();

            try
            {
                var serviceProvider = new ServiceCollection()
                    .AddDbContext<ShopDbContext>(options =>
                         options.UseNpgsql(_configuration.GetConnectionString("DatabaseConnection")))
                    .BuildServiceProvider();

                using (var scope = serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<ShopDbContext>();
                    dbContext.Database.Migrate();
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Migrations applied successfully.");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An error occurred while applying migrations: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}
