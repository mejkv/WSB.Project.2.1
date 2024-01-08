using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirShop.ExternalServices.Entities
{
    public class Receipt
    {
        public int InvoiceNumber { get; set; }
        public required string CustomerName { get; set; }
        public decimal TotalAmount { get; set; }
        public required IList<Device> Devices { get; set; }
    }
}
