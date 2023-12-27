using CrudProduct.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudProduct.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductContext _dbContext;

        public ProductController(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProduct()
        {
            /*
            if(_dbContext.Products == null)
            {
                return NotFound();
            }
            */
            return  _dbContext.Products;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            if(_dbContext.Products == null)
            {
                return NotFound();
            }
            var product = await _dbContext.Products.FindAsync(id);
            if(product == null)
            {
                return NotFound();
            }
            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct), new { id = product.id }, product);

        }

        [HttpPut]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if(id != product.id)
            {
                return BadRequest();
            }
            _dbContext.Entry(product).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if (!ProductAvailable(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }

        private bool ProductAvailable(int id)
        {
            return (_dbContext.Products?.Any(x => x.id == id)).GetValueOrDefault();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if(_dbContext.Products == null)
            {
                return NotFound();
            }
            var product = await _dbContext.Products.FindAsync(id);
            if(product == null)
            {
                return NotFound();
            }
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
