using Microsoft.AspNetCore.Mvc;
using Shop.Core.Entities;
using Shop.Data;
using Shop.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IService<Brand> _brandService;
        public BrandsController(IService<Brand> brandService)
        {
            _brandService = brandService;
        }

        // GET: api/<BrandsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> GetAll()
        {
            return await _brandService.GetAllAsync();
        }

        // GET api/<BrandsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> Get(int? id)
        {
            if (id == null)
            {
                return BadRequest("Id Değeri Boş Olamaz!");
            }
            var model = await _brandService.FindAsync(id.Value);
            if (model == null)
            {
                return NotFound("Id ile Kayıt Bulunamadı!"); 
            }
            return model;
        }

        // POST api/<BrandsController>
        [HttpPost]
        public async Task PostAsync([FromBody] Brand value)
        {
            await _brandService.AddAsync(value);
            await _brandService.SaveAsync();
        }

        // PUT api/<BrandsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Brand value)
        {
            _brandService.Update(value);
             _brandService.Save();
        }

        // DELETE api/<BrandsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var brand = await _brandService.FindAsync(id);
            if (brand != null)
            {
                _brandService.Delete(brand);
                await _brandService.SaveAsync();
                return Ok("Kayıt Silindi!");
            }
            return NotFound("Id ile Kayıt Bulunamadı!");

        }
    }
}
