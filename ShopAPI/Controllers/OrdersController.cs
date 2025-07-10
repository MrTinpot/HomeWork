using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Core.Entities;
using Shop.Data;
using Shop.Service;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class OrdersController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;
        private readonly IService<Orders> _orderService;
        private readonly IService<Product> _productService;
        public OrdersController(DatabaseContext dbContext, IService<Orders> orderService,IService<Product> productService)
        {
            _dbContext = dbContext;
            _orderService = orderService;
            _productService = productService;
        }
        // GET: api/<OrdersController>
        [HttpGet]
        public IEnumerable<Orders> Get()
        {
            var model = _dbContext.Orders
    .Include(c => c.User)
    .Include(c => c.Product);
            return model;
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Orders>> Get(int? id)
        {
            if (id == null)
            {
                return BadRequest("Id Değeri Boş Olamaz!");
            }
            var model = await _orderService.FindAsync(id.Value);
            if (model == null)
            {
                return NotFound("Id ile Kayıt Bulunamadı!");
            }
            return model;
        }
        [HttpGet("userid/{userId}")]
        public async Task<ActionResult<IEnumerable<Orders>>> GetByUserId(int userId)
        {
            var orders = await _dbContext.Orders
                .Include(o => o.User)
                .Include(o => o.Product)
                .Where(o => o.UserId == userId && o.isActive)
                .ToListAsync();

            return Ok(orders);
        }

        // POST api/<OrdersController>
        [HttpPost("admin")]
        [Authorize]
        public async Task PostAsync([FromBody] Orders value)
        {
            await _orderService.AddAsync(value);
            await _orderService.SaveAsync();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PlaceOrder([FromBody] Orders order)
        {
            var product = await _productService.FindAsync(order.ProductId);
            if (product == null)
                return NotFound("Ürün bulunamadı.");

            if (product.Stock < order.Quantity)
                return BadRequest("Yeterli stok yok.");

            // Subtract stock
            product.Stock -= order.Quantity;
            _productService.Update(product);

            // Create order
            await _orderService.AddAsync(order);
            await _orderService.SaveAsync();
            await _productService.SaveAsync();

            return Ok(order);
        }

        // PUT api/<OrdersController>/5
        [HttpPut("{id}")]
        [Authorize]
        public void Put(int id, [FromBody] Orders value)
        {
            _orderService.Update(value);
            _orderService.Save();
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _orderService.FindAsync(id);
            if (order != null)
            {
                var product = await _productService.FindAsync(order.ProductId);
                if (product == null)
                    return NotFound("Ürün bulunamadı.");
                product.Stock += order.Quantity;
                _productService.Update(product);
                _orderService.Delete(order);
                await _orderService.SaveAsync();
                await _productService.SaveAsync();
                return Ok("Kayıt Silindi!");
            }
            return NotFound("Id ile Kayıt Bulunamadı!");

        }
    }
}
