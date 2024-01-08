using System.ComponentModel.DataAnnotations;

namespace AirShop.DataAccess.Data.Models
{
    public class Receipt
    {
        public int ReceiptId { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public decimal Value { get; set; }

        [Required]
        public decimal Vat { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public decimal Discount { get; set; }

        [Required]
        public bool IsSimplifiedInvoice { get; set; }
    }
}