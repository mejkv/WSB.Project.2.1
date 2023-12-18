using AirShop.ExternalServices.Services;
using AirShop.WebApp.ShopContext;
using Microsoft.Extensions.Hosting.Internal;

namespace AirShop.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddScoped<ReceiptService>();
            builder.Services.AddScoped<InvoiceTemplateService>();
            builder.Services.AddHttpContextAccessor();

            //singletons
            builder.Services.AddSingleton<ShoppingCart>();
            builder.Services.AddSingleton<ShopMainContext>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}