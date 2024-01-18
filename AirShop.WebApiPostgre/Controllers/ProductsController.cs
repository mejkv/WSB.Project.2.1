using AirShop.DataAccess.Data.Models;
using AirShop.DataAccess.Data.ShopDbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirShop.WebApiPostgre.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ShopDbContext _context;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ShopDbContext context, ILogger<ProductsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products
                .Include(p => p.Code)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                _logger.LogError($"Not found product with ID: {id}");
                return NotFound();
            }

            _logger.LogInformation($"Product with ID: {id} - {product.Name}");
            return product;
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Product>>> PostProducts(IEnumerable<Product> products)
        {
            if (products == null || !products.Any())
            {
                _logger.LogError($"No products provided for creation.");
                return BadRequest("No products provided for creation.");
            }

            _logger.LogInformation($"Start products import to database");
            foreach (var product in products)
            {
                _logger.LogInformation($"Adding {product.ProductId} to database");

                /*if (product.Code != null)
                    product.Code.Product = product;*/

                _context.Products.Add(product);

                _logger.LogInformation($"Added {product.ProductId} to database");
            }

            await _context.SaveChangesAsync();
            _logger.LogInformation($"End products import to database");

            return CreatedAtAction(nameof(GetProducts), products);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyProduct(int id, Product modifiedProduct)
        {
            var existingProduct = await _context.Products.FindAsync(id);

            if (existingProduct == null)
            {
                _logger.LogError($"Not found product with ID: {id}");
                return NotFound();
            }

            existingProduct.Name = modifiedProduct.Name;
            // Modyfikuj inne właściwości według potrzeb

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
