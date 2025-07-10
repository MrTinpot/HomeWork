using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Core.Entities;
using Shop.Data;

namespace UrunSitesi.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ProductsController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;

        public ProductsController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: api/<ProductController>
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            var model = _dbContext.Products 
                .Include(c => c.Category)  
                .Include(c => c.Brand);
            return model;
        }

        [HttpGet("search/{q}")] 
        public IEnumerable<Product> Search(string q = "")
        {
            var model = _dbContext.Products
                .Where(p => 
                p.Name.Contains(q) || p.Category.Name.Contains(q) || p.Brand.Name.Contains(q))
                .Include(c => c.Category)
                .Include(c => c.Brand);
            return model;
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var model = _dbContext.Products
                .Where(c => c.Id == id)
                .Include(c => c.Category)
                .Include(c => c.Brand)
                .FirstOrDefault();
            if (model != null)
                return model;
            return NotFound("Ürün Bulunamadı!");
        }

        // POST api/<ProductController>
        [HttpPost]
        [Authorize]
        public async Task PostAsync([FromBody] Product value)
        {
            await _dbContext.Products.AddAsync(value);
            await _dbContext.SaveChangesAsync();
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        [Authorize]
        public void Put(int id, [FromBody] Product value)
        {
            _dbContext.Products.Update(value);
            _dbContext.SaveChanges();
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteAsync(int? id)
        {
            if (id == null)
                return BadRequest();
            var model = await _dbContext.Products.FindAsync(id);
            if (model != null)
            {
                _dbContext.Products.Remove(model);
                await _dbContext.SaveChangesAsync();
                return Ok("Ürün Silindi!");
            }
            return NotFound("Ürün Bulunamadı!");
        }
    }
}
