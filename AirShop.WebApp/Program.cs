using AirShop.ExternalServices.Interfaces;
using AirShop.ExternalServices.Services;
using AirShop.ExternalServices.Services.Printout;
using AirShop.ExternalServices.Services.Rest;
using AirShop.WebApp.Pages;
using AirShop.WebApp.ShopContext;
using AirShop.WebApp.Tools;
using Microsoft.Extensions.Hosting.Internal;
using Serilog;

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
            builder.Services.AddScoped<IDocumentService, DocumentInvoiceService>();
            builder.Services.AddScoped<DocumentHelper>();
            builder.Services.AddScoped<BarcodePrinterService>();
            builder.Services.AddScoped<PrintoutService>();
            builder.Services.AddScoped<IAirRestClientConfig, AirRestClientConfig>();
            builder.Services.AddScoped<IAirRestClient, AirRestClient>();

            builder.Services.AddHttpContextAccessor();

            //singletons
            builder.Services.AddSingleton<ShoppingCart>();
            builder.Services.AddSingleton<ShopMainContext>();

            //loging
            builder.Logging.AddSerilog();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            if (app.Environment.IsDevelopment())
            {
                app.UseStaticFiles();
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