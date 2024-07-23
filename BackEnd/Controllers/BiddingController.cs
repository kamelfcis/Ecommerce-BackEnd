using BackEnd.DTO.Bid;
using BackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BiddingController : ControllerBase
    {
        private readonly BotigaContext _context;

        public BiddingController(BotigaContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromForm]AddBidItem item)
        {
            var newrole = new BidItem()
            {
                ItemName = item.ItemName,
                ItemDescription = item.ItemDescription,
                BiddingDate = item.BiddingDate,
                Image = item.Image,
                InsurancePrice = item.InsurancePrice,

            };
            await _context.AddAsync(newrole);
            await _context.SaveChangesAsync();
            return Ok(newrole);
        }
        [HttpPost("CreateBid")]
        public async Task<IActionResult> Post(CreateBid bid)
        {

            var newrole = new BiddingTransaction()
            {
                TransactionBidValue = bid.TransactionBidValue,
                BidItemId = bid.BidItemId,
                UserId = bid.UserId,

            };
            await _context.AddAsync(newrole);
            await _context.SaveChangesAsync();
            return Ok(newrole);
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            var Items = await _context.BidItems.ToListAsync();

            return Ok(Items);
        }

        [HttpGet("GetBidders/{id}")]
        public async Task<IActionResult> GetBidders(int id)
        {

            var Items = await _context.BiddingTransaction.Include(x=>x.User).Where(x=>x.BidItemId==id).ToListAsync();

            return Ok(Items);
        }
        [HttpGet("GetSingleBidItem/{id}")]
        public async Task<IActionResult> GetSingleBidItem(int id)
        {

            var Items = await _context.BidItems.FirstOrDefaultAsync(p => p.Id == id);

            return Ok(Items);
        }
    }
}
