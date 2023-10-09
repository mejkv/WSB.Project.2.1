using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazynEdu.DataAccess.Entities
{
    public class Device : EntityBase
    {
        public int DeviceCaseId { get; set; }

        public DeviceCase DeviceCase { get; set; }

        public List<Author> Authors { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        public int Year { get; set; }
    }
}
