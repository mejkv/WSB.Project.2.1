﻿using System.ComponentModel.DataAnnotations;

namespace AirShop.WebApiPostgre.Data.Entities
{
    public class ReceiptDao
    {
        public int ReceiptId { get; set; }

        public string Name { get; set; }

        public decimal Value { get; set; }

        public decimal Vat { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public bool IsSimplifiedInvoice { get; set; }
    }
}
