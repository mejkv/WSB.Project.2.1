using System;
using System.Collections.Generic;
using System.Linq;
using AirShop.DataAccess.Data.Models;
using AirShop.ExternalServices.Services;
using AirShop.WebApp.ShopContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AirShop.WebApp.Pages
{
    public class ShoppingCartModel : PageModel
    {
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
        public decimal TotalPrice { get; set; }

        private readonly ShoppingCart _shoppingCart;
        private readonly ReceiptService _receiptService;

        public ShoppingCartModel(ShoppingCart shoppingCart, ReceiptService receiptService)
        {
            _shoppingCart = shoppingCart;
            _receiptService = receiptService;

            ShoppingCartItems = new List<ShoppingCartItem>();
        }

        public void OnGet()
        {
            foreach (var product in _shoppingCart.CartItems)
            {
                var item = new ShoppingCartItem()
                {
                    ProductId = product.ProductId,
                    ProductName = product.Name,
                    Price = product.Price,
                    SourceProduct = product,
                };
                ShoppingCartItems.Add(item);
            }

            TotalPrice = ShoppingCartItems.Sum(item => item.Price);
        }

        /*public IActionResult OnPostProceedCheckout()
        {
            try
            {
                _receiptService.ReturnUserReceipt(_shoppingCart.CartItems.ToList());
                return new JsonResult(new { success = true });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, error = ex.Message });
            }
        }*/
    }

    public class ShoppingCartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public Product SourceProduct { get; set; }
    }
}
