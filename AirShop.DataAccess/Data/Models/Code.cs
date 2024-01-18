using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AirShop.DataAccess.Data.Models
{
    public class Code
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

       // public Product Product { get; set; }

        public string Ean { get; set; }
    }
}