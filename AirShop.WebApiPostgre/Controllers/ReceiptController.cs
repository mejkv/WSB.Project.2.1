using AirShop.DataAccess.Data.Models;
using AirShop.DataAccess.Data.ShopDbContext;
using Microsoft.AspNetCore.Mvc;

namespace AirShop.WebApiPostgre.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptController : Controller
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
            _context.Receipts.Add(receipt);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetReceipt), new { id = receipt.ReceiptId }, receipt);
        }
    }
}
