using BackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStatusesController : ControllerBase
    {
        private readonly BotigaContext _context;

        public OrderStatusesController(BotigaContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> Post(OrderStatus role)
        {
            var newOrderStatus = new OrderStatus()
            {
                StatusName = role.StatusName,
            };
            await _context.AddAsync(newOrderStatus);
            await _context.SaveChangesAsync();
            return Ok(newOrderStatus);
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            var Users = await _context.OrderStatuses.ToListAsync();

            return Ok(Users);
        }
        [HttpPut("Id")]
        public async Task<IActionResult> Put(int Id, OrderStatus register)
        {
            var UpdatedOrderStatus= await _context.OrderStatuses.FirstOrDefaultAsync(u => u.Id == Id);
            UpdatedOrderStatus.StatusName = register.StatusName;

            await _context.SaveChangesAsync();
            return Ok("Updated Successfully");
        }
        [HttpDelete("Id")]
        public async Task<IActionResult> Delete(int Id)
        {
            var DeleteStatus = await _context.OrderStatuses.FirstOrDefaultAsync(u => u.Id == Id);
            _context.Remove(DeleteStatus);
            await _context.SaveChangesAsync();
            return Ok("Deleted Successfully");
        }
    }
}
