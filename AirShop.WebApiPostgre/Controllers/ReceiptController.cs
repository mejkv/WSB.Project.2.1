using AirShop.DataAccess.Data.Models;
using AirShop.DataAccess.Data.ShopDbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirShop.WebApiPostgre.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ReceiptController : ControllerBase
    {
        private readonly ShopDbContext _context;

        public ReceiptController(ShopDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetReceipts()
        {
            var receipts = _context.Receipts.ToList();
            return Ok(receipts);
        }

        [HttpGet("{id}")]
        public IActionResult GetReceipt(int id)
        {
            var receipt = _context.Receipts.Find(id);

            if (receipt == null)
            {
                return NotFound();
            }

            return Ok(receipt);
        }

        [HttpPost]
        public IActionResult CreateReceipt([FromBody] Receipt receipt)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Receipts.Add(receipt);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetReceipt), new { id = receipt.ReceiptId }, receipt);
        }
    }
}
