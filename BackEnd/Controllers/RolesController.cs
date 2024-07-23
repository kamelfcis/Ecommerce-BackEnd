using BackEnd.DTO.Category;
using BackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly BotigaContext _context;

        public RolesController(BotigaContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> Post(Role role)
        {
            var newrole = new Role()
            {
                Name = role.Name,
            };
            await _context.AddAsync(newrole);
            await _context.SaveChangesAsync();
            return Ok(newrole);
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            var Users = await _context.Roles.ToListAsync();
            
            return Ok(Users);
        }
        [HttpPut("Id")]
        public async Task<IActionResult> Put(int Id, Role register)
        {
            var UpdatedRole = await _context.Roles.FirstOrDefaultAsync(u => u.Id == Id);
            UpdatedRole.Name = register.Name;

            await _context.SaveChangesAsync();
            return Ok("Updated Successfully");
        }
        [HttpDelete("Id")]
        public async Task<IActionResult> Delete(int Id)
        {
            var DeleteRole = await _context.Roles.FirstOrDefaultAsync(u => u.Id == Id);
            _context.Remove(DeleteRole);
            await _context.SaveChangesAsync();
            return Ok("Deleted Successfully");
        }
    }
}
