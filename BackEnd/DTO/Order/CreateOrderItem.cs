using BackEnd.Models;

namespace BackEnd.DTO.Order
{
    public class CreateOrderItem
    {
       
        public int ProductId { get; set; }
         
        public int Quantity { get; set; }
    }
}
