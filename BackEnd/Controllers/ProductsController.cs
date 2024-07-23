using BackEnd.DTO.Emergency;
using BackEnd.Models;
using BackEnd.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly BotigaContext _dbContext;
        private readonly IFileService _fileService;
        private new List<string> _allowedExtenstions = new List<string> { ".jpg", ".png" };
        private long _maxAllowedPosterSize = 1048576;
        public ProductsController(BotigaContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] CreateProductDTO dto)
        {
            if (!_allowedExtenstions.Contains(Path.GetExtension(dto.Image.FileName).ToLower()))
                return BadRequest("Only .png and .jpg images are allowed!");

            if (dto.Image.Length > _maxAllowedPosterSize)
            {
                return BadRequest("Max allowed size for image is 1MB");
            }
            var isvalidCategory = await _dbContext.Categories.AnyAsync(c => c.Id == dto.CategoryId);
            if (!isvalidCategory)
            {
                return BadRequest("Invalid Category ID");
            }
            using var datastream = new MemoryStream();
            await dto.Image.CopyToAsync(datastream);

            var fileResult = _fileService.SaveImage(dto.Image);
            if (fileResult.Item1 == 1)
            {
                var product = new Product
                {
                    CategoryId = dto.CategoryId,
                    ProductName = dto.ProductName,
                    Description = dto.Description,
                    Price = dto.Price,
                    UserId = dto.UserId,
                    ImageStr = fileResult.Item2,
                    Image = datastream.ToArray(),
                    BestSeller = dto.BestSeller,
                    Featured = dto.Featured,
                    OnBidding = dto.OnBidding,
                    InStock=dto.InStock,
                    Rate=dto.Rate, 

                }; await _dbContext.AddAsync(product);

            }


            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        public IActionResult Get()
        {
            var Posts = from p in _dbContext.Products.Include(p => p.Category).Include(x => x.User).ToList()
                        select new GetProductDTO
                        {
                            Id = p.Id,
                            CategoryName = p.Category.Name,

                            CategoryId = p.Category.Id,
                            ProductName = p.ProductName,
                            Description = p.Description,
                            Image = "" + _fileService.GetFilePath(p.ImageStr),
                            ImageBin=p.Image,
                      
                            UserId = p.UserId,
                            UserName = p.User.Username,
                            OnBidding = p.OnBidding,
                            Featured = p.Featured,
                            BestSeller = p.BestSeller,
                            Price = p.Price,
                            Rate=p.Rate, 
                            InStock=p.InStock,

                        };

            return Ok(Posts);

        }
        [HttpGet("OnBidProducts")]
        public IActionResult OnBidProducts()
        {
            var Posts = from p in _dbContext.Products.Include(p => p.Category).Include(x => x.User).Where(x=>x.OnBidding==true).ToList()
                        select new GetProductDTO
                        {
                            Id = p.Id,
                            CategoryName = p.Category.Name,

                            CategoryId = p.Category.Id,
                            ProductName = p.ProductName,
                            Description = p.Description,
                            Image = "" + _fileService.GetFilePath(p.ImageStr),
                            ImageBin = p.Image,

                            UserId = p.UserId,
                            UserName = p.User.Username,
                            OnBidding = p.OnBidding,
                            Featured = p.Featured,
                            BestSeller = p.BestSeller,
                            Price = p.Price,
                            Rate = p.Rate,
                            InStock = p.InStock,

                        };

            return Ok(Posts);

        }

        [HttpGet("BestSellerProducts")]
        public IActionResult BestSellerProducts()
        {
            var Posts = from p in _dbContext.Products.Include(p => p.Category).Include(x => x.User).Where(x => x.BestSeller == true).ToList()
                        select new GetProductDTO
                        {
                            Id = p.Id,
                            CategoryName = p.Category.Name,

                            CategoryId = p.Category.Id,
                            ProductName = p.ProductName,
                            Description = p.Description,
                            Image = "" + _fileService.GetFilePath(p.ImageStr),
                            ImageBin = p.Image,

                            UserId = p.UserId,
                            UserName = p.User.Username,
                            OnBidding = p.OnBidding,
                            Featured = p.Featured,
                            BestSeller = p.BestSeller,
                            Price = p.Price,
                            Rate = p.Rate,
                            InStock = p.InStock,

                        };

            return Ok(Posts);

        }

        [HttpGet("GetSingle/{id}")]
        public IActionResult GetSingle(int id)
        {
            // Validate input
            if (id <= 0)
            {
                return BadRequest("Invalid product ID.");
            }

            // Retrieve the product
            var singleProduct = _dbContext.Products.Include(p => p.Category).Include(x => x.User)
        .FirstOrDefault(p => p.Id == id);

            if (singleProduct != null)
            {
                var productDTO = new GetProductDTO
                {
                    Id = singleProduct.Id,
                    CategoryName = singleProduct.Category.Name,
                    CategoryId = singleProduct.Category.Id,
                    ProductName = singleProduct.ProductName,
                    Description = singleProduct.Description,
                    Image = "" + _fileService.GetFilePath(singleProduct.ImageStr),
                    UserId = singleProduct.UserId,
                    UserName = singleProduct.User.Username,
                    OnBidding = singleProduct.OnBidding,
                    Featured = singleProduct.Featured,
                    BestSeller = singleProduct.BestSeller,
                    Price = singleProduct.Price,
                    Rate = singleProduct.Rate,
                    ImageBin = singleProduct.Image,
                    InStock = singleProduct.InStock,
                };

                return Ok(productDTO);
            }
            else
            {
                return NotFound("Product not found.");
            }


            // Check if the product exists
            if (singleProduct == null)
            {
                return NotFound("Product not found.");
            }

            // Return the product with a 200 OK response    
            return Ok(singleProduct);
        }



        [HttpGet("{id}")]
        public IActionResult GetByCategory(int id)
        {

            var Posts = from p in _dbContext.Products.Include(p => p.Category).Include(x => x.User).Where(x => x.CategoryId == id).ToList()
                        select new GetProductDTO
                        {
                            Id = p.Id,
                            CategoryName = p.Category.Name,
                            CategoryId = p.Category.Id,

                            ProductName = p.ProductName,
                            Description = p.Description,
                            Image = "" + _fileService.GetFilePath(p.ImageStr),
                            UserId = p.UserId,
                            UserName = p.User.Username,
                            OnBidding = p.OnBidding,
                            Featured = p.Featured,
                            BestSeller = p.BestSeller,
                            Price = p.Price,  
                            Rate= p.Rate,
                            ImageBin=p.Image,
                            InStock = p.InStock,
                        };

            return Ok(Posts);

        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var DeleteProduct = await _dbContext.Products.FirstOrDefaultAsync(u => u.Id == Id);
            _dbContext.Remove(DeleteProduct);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
