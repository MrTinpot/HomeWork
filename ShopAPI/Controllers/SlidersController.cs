using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Core.Entities;
using Shop.Core.Models;
using Shop.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlidersController : ControllerBase
    {
        private readonly IService<Slider> _sliderService;
        public SlidersController(IService<Slider> sliderService)
        {
            _sliderService = sliderService;
        }

        // GET: api/<SlidersController>

        [HttpGet]
        public async Task<IEnumerable<Slider>> GetAll()
        {
            return await _sliderService.GetAllAsync();
        }

        // GET api/<SlidersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Slider>> Get(int? id)
        {
            if (id == null)
            {
                return BadRequest("Id Değeri Boş Olamaz!");
            }
            var model = await _sliderService.FindAsync(id.Value);
            if (model == null)
            {
                return NotFound("Id ile Kayıt Bulunamadı!");
            }
            return model;
        }

        // POST api/<SlidersController>
        [HttpPost]
        [Authorize]
        public async Task PostAsync([FromBody] Slider value)
        {
            await _sliderService.AddAsync(value);
            await _sliderService.SaveAsync();
        }

        // PUT api/<SlidersController>/5
        [HttpPut("{id}")]
        [Authorize]

        public void Put(int id, [FromBody] Slider value)
        {
            _sliderService.Update(value);
            _sliderService.Save();
        }

        // DELETE api/<SlidersController>/5
        [HttpDelete("{id}")]
        [Authorize]

        public async Task<IActionResult> Delete(int id)
        {
            var slider = await _sliderService.FindAsync(id);
            if (slider != null)
            {
                _sliderService.Delete(slider);
                await _sliderService.SaveAsync();
                return Ok("Silme İşlemi Başarılı!");
            }                
            return NotFound("Id ile Kayıt Bulunamadı!");
        }
    }
}
