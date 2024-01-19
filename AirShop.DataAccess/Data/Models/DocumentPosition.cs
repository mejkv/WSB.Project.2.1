using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AirShop.DataAccess.Data.Models
{
    public class DocumentPosition
    {
        [JsonIgnore]
        public int DocumentPositionId { get; set; }
        [JsonIgnore]
        public int DocumentId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
