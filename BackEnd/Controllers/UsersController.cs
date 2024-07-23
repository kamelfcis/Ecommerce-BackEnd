using BackEnd.DTO.Auth;
using BackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly BotigaContext _context;

        public UsersController(BotigaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        { 
            var Users = await _context.Users.Include(r=>r.Role).ToListAsync(); 
            return Ok(Users);
        }
        [HttpGet("GetByID/{id}")]
        public  IActionResult GetByID(int id)
        {

            var Users = _context.Users.SingleOrDefault(u => u.Id == id);

            return Ok(Users);
        }

        [HttpPut("Id")]
        public async Task<IActionResult> Put(int Id, Register register)
        {
            var UpdatedUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == Id);
            UpdatedUser.Email = register.Email;
            UpdatedUser.Password = register.Password;
            UpdatedUser.FullName = register.FullName;
            UpdatedUser.Phone = register.Phone;
            await _context.SaveChangesAsync();
            return Ok("Updated Successfully");
        }
        [HttpDelete("Id")]
        public async Task<IActionResult> Delete(int Id)
        {
            var DeleteUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == Id);
            _context.Remove(DeleteUser);
            await _context.SaveChangesAsync();
            return Ok("Deleted Successfully");
        }

    }
}
