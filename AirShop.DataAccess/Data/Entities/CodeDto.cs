using System.ComponentModel.DataAnnotations;

namespace AirShop.WebApiPostgre.Data.Entities
{
    public class CodeDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public required string Ean { get; set; }

        public ProductDto Product { get; set; }
    }
}
