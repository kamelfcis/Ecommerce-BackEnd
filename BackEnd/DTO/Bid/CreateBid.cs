using BackEnd.Models;

namespace BackEnd.DTO.Bid
{
    public class CreateBid
    {
         public int UserId { get; set; }


        public int BidItemId { get; set; }
        public double TransactionBidValue { get; set; }
    }
}
