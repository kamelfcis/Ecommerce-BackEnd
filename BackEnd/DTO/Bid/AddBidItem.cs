using System;

namespace BackEnd.DTO.Bid
{
    public class AddBidItem
    {
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public string ItemPrice { get; set; }

        public string InsurancePrice { get; set; }
        public string Image { get; set; }

        public DateTime BiddingDate { get; set; }
    }
}
