using BackEnd.Models;
using System.Collections.Generic;
using System;

namespace BackEnd.DTO.Order
{
    public class CreateOrder
    {
         
        public int UserId { get; set; }
        
        public List<CreateOrderItem> OrderItems { get; set; } // List of items in the order
       
        public DateTime OrderDate { get; set; } = DateTime.Now;

       
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
