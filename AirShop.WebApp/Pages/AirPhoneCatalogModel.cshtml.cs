using AirShop.ExternalServices.Services;
using AirShop.ExternalServices.Services.Rest;
using AirShop.WebApp.ShopContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AirShop.WebApp.Pages
{
    public class AirPhoneCatalogModelModel : CatalogModelBase
    {
        public AirPhoneCatalogModelModel(
            ShoppingCart shoppingCart, 
            ReceiptService receiptService, 
            IAirRestClient client) 
            : base(shoppingCart, receiptService, client)
        {
        }
    }
}
