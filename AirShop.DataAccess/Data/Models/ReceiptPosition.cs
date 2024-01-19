namespace AirShop.DataAccess.Data.Models
{
    public class ReceiptPosition
    {
        public int ReceiptPositionId { get; set; }
        public int ReceiptId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
