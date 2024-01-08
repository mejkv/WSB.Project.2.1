using AirShop.DataAccess.Data.Models;
using AirShop.DataAccess.Data.ShopDbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirShop.WebApiPostgre.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractorController : ControllerBase
    {
        private readonly ShopDbContext _context;

        public ContractorController(ShopDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contractor>>> GetContractors()
        {
            return await _context.Contractor.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Contractor>> GetContractor(int id)
        {
            var contractor = await _context.Contractor.FindAsync(id);

            if (contractor == null)
            {
                return NotFound();
            }

            return contractor;
        }

        [HttpPost]
        public async Task<ActionResult<Contractor>> PostContractor(Contractor contractor)
        {
            _context.Contractor.Add(contractor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetContractor), new { id = contractor.ContractorId }, contractor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutContractor(int id, Contractor contractor)
        {
            if (id != contractor.ContractorId)
            {
                return BadRequest();
            }

            _context.Entry(contractor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContractorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContractor(int id)
        {
            var contractor = await _context.Contractor.FindAsync(id);
            if (contractor == null)
            {
                return NotFound();
            }

            _context.Contractor.Remove(contractor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContractorExists(int id)
        {
            return _context.Contractor.Any(e => e.ContractorId == id);
        }
    }
}
