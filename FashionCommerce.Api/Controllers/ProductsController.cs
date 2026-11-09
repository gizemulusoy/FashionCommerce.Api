using FashionCommerce.Api.Data;
using FashionCommerce.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FashionCommerce.Api.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Receive the database context from ASP.NET Core.
        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // Get all products from the database.
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _context.Products
                .AsNoTracking()
                .OrderBy(product => product.Id)
                .ToListAsync();

            return Ok(products);
        }

        // Find a product by its ID.
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(product => product.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
    }
}