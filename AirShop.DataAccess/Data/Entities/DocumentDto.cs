using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirShop.DataAccess.Data.Entities
{
    public class DocumentDto
    {
        public int UserId { get; set; }
        public int ContractorId { get; set; }
        public DateTime IssueDate { get; set; }
        public List<DocumentPositionDto> DocumentPositions { get; set; } = new List<DocumentPositionDto>();
    }
}
