namespace BackEnd.Models
{
    public class BiddingTransaction
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public BidItem BidItem { get; set; }
        public int BidItemId { get; set; }
        public User User { get; set; }
        public double TransactionBidValue { get; set; }
    }
}
