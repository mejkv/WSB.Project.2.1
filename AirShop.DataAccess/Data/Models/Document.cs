using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirShop.DataAccess.Data.Models
{
    public class Document
    {
        public int DocumentId { get; set; }
        public int UserId { get; set; }
        public int ContractorId { get; set; }
        public DateTime IssueDate { get; set; }
        public Contractor Contractor { get; set; }
        //public User User { get; set; }
        public List<DocumentPosition> DocumentPositions { get; set; }
    }
}
