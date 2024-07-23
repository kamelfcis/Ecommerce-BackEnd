using BackEnd.DTO.Auth;
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
    public class CategoriesController : ControllerBase
    {
        private readonly BotigaContext _context;

        public CategoriesController(BotigaContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> Post(AddCategory category)
        {
            var newcategory = new Category()
            {
                Name = category.Name,
            };
            await _context.AddAsync(newcategory);
            await _context.SaveChangesAsync();
            return Ok(newcategory);
        }
            [HttpGet]
        public async Task<IActionResult> Get()
        {

            var Users = await _context.MainCategories.Include(c=>c.Categories).ToListAsync();

            return Ok(Users);
        }
        [HttpPut("Id")]
        public async Task<IActionResult> Put(int Id, Category register)
        {
            var UpdatedCategory = await _context.Categories.FirstOrDefaultAsync(u => u.Id == Id);
            UpdatedCategory.Name = register.Name;
           
            await _context.SaveChangesAsync();
            return Ok("Updated Successfully"); 
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMainCategory(int id)
        {
            var mainCategory = await _context.MainCategories.FindAsync(id);
            if (mainCategory == null)
            {
                return NotFound(); // Return 404 Not Found if the resource is not found
            }

            _context.MainCategories.Remove(mainCategory);
            await _context.SaveChangesAsync();
 
            return NoContent(); // Return 204 No Content upon successful deletion
        }

    }
}
