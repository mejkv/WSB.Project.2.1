using AirShop.ExternalServices.Services;
using AirShop.WebApiPostgre.Data.Models;

namespace AirShop.WebApp.ShopContext
{
    public class ShoppingCart
    {
        private List<Product> _cartItems;
        public ShoppingCart()
        {
            _cartItems = new List<Product>();
        }

        public IReadOnlyList<Product> CartItems => _cartItems.AsReadOnly();

        public void AddToCart(Product product)
        {
            _cartItems.Add(product);
        }

        public void RemoveFromCart(int productId)
        {
            var productToRemove = _cartItems.FirstOrDefault(p => p.ProductId == productId);
            if (productToRemove != null)
            {
                _cartItems.Remove(productToRemove);
            }
        }

        public void ClearCart()
        {
            _cartItems.Clear();
        }

        public int GetCartItemCount()
        {
            return _cartItems.Count();
        }
    }
}