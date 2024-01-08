using AirShop.DataAccess.Data.Models;
using AirShop.ExternalServices;
using AirShop.ExternalServices.Services;
using AirShop.ExternalServices.Services.Rest;

using AirShop.WebApp.ShopContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AirShop.WebApp.Pages
{
    public class GetAndPostMethodModel : PageModel
    {
        private ShoppingCart _shoppingCart;
        private readonly ReceiptService _receiptService;

        public List<Product> Products { get; set; }

        public GetAndPostMethodModel(ShoppingCart shoppingCart, ReceiptService receiptService)
        {
            _shoppingCart = shoppingCart;
            _receiptService = receiptService;
            Products = GetProducts().ToList();
        }

        public void OnGet()
        {
        }

        public IEnumerable<Product> GetProducts()
        {
            var client = new AirRestClient(new AirRestClientConfig());
            var products = client.GetDevices();
            return products;
        }

        public IActionResult OnPostAddToCart(int productId)
        {
            var productToAdd = Products.FirstOrDefault(p => p.ProductId == productId);
            if (productToAdd != null)
            {
                _shoppingCart.AddToCart(productToAdd);
                //_receiptService.ReturnUserRecipt(new List<Product>() { productToAdd });
                return RedirectToPage("ShoppingCart");
            }
            return Page();
        }

        public void RemoveFromCart(int productId)
        {
            _shoppingCart.RemoveFromCart(productId);
        }

        public void ClearCart()
        {
            _shoppingCart.ClearCart();
        }

        public int GetCartItemCount()
        {
            // Tutaj implementuj logikę pobierania liczby przedmiotów w koszyku
            // Może to być wywołanie metody w ShoppingCart lub pobranie wartości z cache, itp.
            return _shoppingCart.GetCartItemCount();
        }
    }
}
