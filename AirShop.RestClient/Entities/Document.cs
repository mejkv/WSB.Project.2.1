using AirShop.DataAccess.Data.Entities;
using AirShop.DataAccess.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirShop.ExternalServices.Entities
{
    public class Document
    {
        public User User { get; set; }
        public Customer Customer { get; set; }
        public DateTime CreationTime { get; set; }
        public List<DocumentPosition> DocumentPositions { get; set; } = new List<DocumentPosition>();
    }

    public class DocumentPosition
    {
        public int DocumentPositionId { get; set; }
        public int DocumentId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public Document Document { get; set; }
        public Product Product { get; set; }
    }
}
