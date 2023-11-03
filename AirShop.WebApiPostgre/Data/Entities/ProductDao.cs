namespace AirShop.WebApiPostgre.Data.Entities
{
    public class ProductDao
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public decimal Value { get; set; }

        public decimal Vat { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }
    }
}
