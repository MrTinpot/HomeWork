using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Core.Entities;
using Shop.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class CategoriesController : ControllerBase
    {
        private readonly IService<Category> _CategoryService;
        public CategoriesController(IService<Category> categoryService)
        {
            _CategoryService = categoryService;
        }
        // GET: api/<CategoriesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAll()
        {
            return await _CategoryService.GetAllAsync();
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> Get(int? id)
        {
            if (id == null)
            {
                return BadRequest("Id Değeri Boş Olamaz!");
            }
            var model = await _CategoryService.FindAsync(id.Value);
            if (model == null)
            {
                return NotFound("Id ile Kayıt Bulunamadı!");
            }
            return model;
        }
        // POST api/<CategoriesController>
        [HttpPost]
        [Authorize]
        public async Task PostAsync([FromBody] Category value)
        {
            await _CategoryService.AddAsync(value);
            await _CategoryService.SaveAsync();
        }


        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        [Authorize]
        public void Put(int id, [FromBody] Category value)
        {
            _CategoryService.Update(value);
            _CategoryService.Save();
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var cate = await _CategoryService.FindAsync(id);
            if (cate != null)
            {
                _CategoryService.Delete(cate);
                await _CategoryService.SaveAsync();
                return Ok("Kayıt Silindi!");
            }
            return NotFound("Id ile Kayıt Bulunamadı!");

        }
    }
}
