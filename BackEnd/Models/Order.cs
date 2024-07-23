using System;
using System.Collections.Generic;
using System.Linq;

namespace BackEnd.Models
{
    public class Order
    {
        public Order()
        {
            OrderItems = new List<OrderItem>(); // Initialize OrderItems to an empty list
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<OrderItem> OrderItems { get; set; } // List of items in the order
        public double TotalPrice => OrderItems?.Sum(item => item.Product.Price * item.Quantity) ?? 0;
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public OrderStatus OrderStatus { get; set; }
        public int OrderStatusId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Country { get; set; }

        public string Address { get; set; }

        public string Apartment { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
