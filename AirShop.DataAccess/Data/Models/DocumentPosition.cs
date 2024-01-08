using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirShop.DataAccess.Data.Models
{
    public class DocumentPosition
    {
        public int DocumentPositionId { get; set; }
        public int DocumentId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public Document Document { get; set; }
        public Product Product { get; set; }
    }
}
