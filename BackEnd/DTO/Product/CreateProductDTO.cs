using BackEnd.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.DTO.Emergency
{
    public class CreateProductDTO
    {

        public int Id { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }

        public double Price { get; set; }
        [Required]
        public IFormFile? Image { get; set; }
        public int CategoryId { get; set; }

        public bool OnBidding { get; set; }
        public bool Featured { get; set; }

        public bool BestSeller { get; set; } 
        public int UserId { get; set; }

        public int Rate { get; set; }
        public bool InStock { get; set; }

    }
}
