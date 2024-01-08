namespace AirShop.DataAccess.Data.Models
{
    public class ReceiptPosition
    {
        public int ReceiptPositionId { get; set; }
        public int ReceiptId { get; set; }
        public required Receipt Receipt { get; set; }

        public int ProductId { get; set; }
        public required Product Product { get; set; }

        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
