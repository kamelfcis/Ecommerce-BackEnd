using BackEnd.DTO.Order;
using BackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly BotigaContext _context;

        public CartController(BotigaContext context)
        {
            _context = context;
        }
        [HttpPost]
        public   IActionResult Post(CreateOrder obj)
        {
            try
            {
                // Create a new Order object
                var order = new Order
                {
                    UserId = obj.UserId,
                    OrderDate = obj.OrderDate,
                    OrderStatusId = obj.OrderStatusId,
                    Address = obj.Address,
                    Apartment = obj.Apartment,  
                    Country = obj.Country,  
                    Email  = obj.Email,
                    FirstName  = obj.FirstName,
                    LastName = obj.LastName,
                    Phone = obj.Phone, 
                }; 
                // Add OrderItems to the Order
                foreach (var orderItemDto in obj.OrderItems)
                {
                    var product = _context.Products.Find(orderItemDto.ProductId);
                    if (product == null)
                    {
                        return BadRequest($"Product with ID {orderItemDto.ProductId} not found.");
                    }

                    var orderItem = new OrderItem
                    {
                        ProductId = orderItemDto.ProductId,
                        Quantity = orderItemDto.Quantity,
                        Product = product // Set the navigation property
                    };

                    order.OrderItems.Add(orderItem);
                }

                // Calculate TotalPrice (assuming you still want to do it at this point)

                // Add Order to the context and save changes
                _context.Orders.Add(order);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
        [HttpGet]
        public IActionResult Get()
        {
            var Orders = _context.Orders.Include(x => x.OrderItems).ThenInclude(z=>z.Product);

            return Ok(Orders);
        }
        [HttpGet("GetOrder/{userid}")]
        public IActionResult Get(int userid)
        {
            var Orders = _context.Orders.Include(x => x.OrderItems).ThenInclude(z => z.Product).Where(x=>x.UserId==userid);

            return Ok(Orders);
        }

        [HttpGet("GetTotalSales/{userid}")]
        public IActionResult GetTotalSales(int userid)
        {
            var totalSales = _context.Orders.Include(o => o.OrderItems).ThenInclude(p => p.Product).Where(u=>u.UserId==userid)
                                   .Select(o => new
                                   {
                                       TotalPrice = o.OrderItems.Sum(oi => oi.Quantity * oi.Product.Price)
                                   })
                                   .ToList()
                                   .Sum(o => o.TotalPrice);
            return Ok(totalSales);
        }
        [HttpGet("GetNumberOfOrders/{userid}")]
        public IActionResult GetNumberOfOrders(int userid)
        {
            var totalSales = _context.Orders.Include(o => o.OrderItems).ThenInclude(p => p.Product).Where(u => u.UserId == userid)
                                
                                   .ToList()
                                   .Count( );
            return Ok(totalSales);
        }

    }

   
}
