using AirShop.DataAccess.Data.Models;

namespace AirShop.WebApiPostgre.Data.Entities
{
    public class ProductDto
    {
        public int ProductId { get; set; }

        public required string Name { get; set; }

        public decimal Value { get; set; }

        public decimal Vat { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public CodeDto? Code { get; set; }

        public ProductType ProductType { get; set; }
    }
}
