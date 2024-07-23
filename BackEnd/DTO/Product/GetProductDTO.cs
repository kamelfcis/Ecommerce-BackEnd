using BackEnd.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.DTO.Emergency
{
    public class GetProductDTO
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public byte[] ImageBin { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
         
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public bool OnBidding { get; set; }
        public bool Featured { get; set; } 
        public bool BestSeller { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }
        public int Rate {  get; set; }
        public bool InStock { get; set; }
    }
}
