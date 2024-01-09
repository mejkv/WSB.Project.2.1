using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirShop.DataAccess.Data.Models
{
    public class Contractor
    {
        public int ContractorId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string? Street { get; set; }
        [Required]
        public required string City { get; set; }
        [Required] 
        public required string HouseNumber { get; set; }
        [Required]
        public string? ApartmentNumber { get; set; }
        [Required]
        public required string PostalCode { get; set; }
        [Required]
        public string? Nip { get; set; }
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
