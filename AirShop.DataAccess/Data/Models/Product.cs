using System.ComponentModel.DataAnnotations;

namespace AirShop.DataAccess.Data.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public required string Name { get; set; }

        public decimal Value { get; set; }

        public decimal Vat { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public Code? Code { get; set; }

        public ProductType ProductType { get; set; }
    }

    public class ProductList
    {
        public List<Product> Products { get; set;}
    }
}