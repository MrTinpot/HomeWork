using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Shop.Core.Entities;
using Shop.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ContactsController : ControllerBase
    {
        private readonly IService<Contact> _contactService;
        public ContactsController(IService<Contact> contactService)
        {
            _contactService = contactService;
        }
        // GET: api/<ContactsController>
        [HttpGet]
        public async Task<IEnumerable<Contact>> Get()
        {
            return await _contactService.GetAllAsync();
        }

        // GET api/<ContactsController>/5
        [HttpGet("{id}")]
        public ActionResult<Contact> Get(int id)
        {
            var contact = _contactService.Find(id);
            if (contact != null)
            {
                return contact;
            }
            return NotFound("Id ile Mesaj Bulunamadı!");
        }

        // POST api/<ContactsController>
        [HttpPost]
        
        public async Task Post([FromBody] Contact value)
        {
            await _contactService.AddAsync(value);
            await _contactService.SaveAsync();
        }

        // PUT api/<ContactsController>/5
        [HttpPut("{id}")]
        [Authorize]
        public void Put(int id, [FromBody] Contact value)
        {
            _contactService.Update(value);
            _contactService.Save();
        }

        // DELETE api/<ContactsController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var contact = _contactService.Find(id);
            if (contact != null)
            {
                _contactService.Delete(contact);
                _contactService.Save();
                return Ok("Mesaj Silindi!");

            }
            return NotFound("Id ile Mesaj Bulunamadı!");
        }
    }
}
