using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Core.Entities;
using Shop.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IService<User> _userService;
        public UsersController(IService<User> userService)
        {
            _userService = userService;
        }
        // GET: api/<UsersController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _userService.GetAllAsync();
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var users = await _userService.FindAsync(id);
            if (users != null)
            {
                return users;
            }
            return NotFound("Kullanıcı Bulunamadı!");
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult<User>> PostUser([FromBody] User value)
        {
            await _userService.AddAsync(value);
            await _userService.SaveAsync();
            return CreatedAtAction(nameof(GetUser), new { id = value.Id }, value);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, [FromBody] User value)
        {
            if (id != value.Id)
            {
                return BadRequest("Id Değerleri Eşleşmiyor!");
            }

            var existingUser = await _userService.FindAsync(id);
            try
            {
                _userService.Update(value);
                await _userService.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (existingUser is null)
                {
                    return NotFound("Kullanıcı Bulunamadı!");
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userService.FindAsync(id);
            if (user == null)
            {
                return NotFound("Kullanıcı Bulunamadı!");
            }
            _userService.Delete(user);
            await _userService.SaveAsync();
            return NoContent();
        }
    }
}
