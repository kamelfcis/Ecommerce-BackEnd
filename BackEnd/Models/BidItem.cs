using System;
using System.Collections.Generic;

namespace BackEnd.Models
{
    public  class BidItem
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public string ItemPrice { get; set; }

        public string InsurancePrice { get; set; }
        public string Image { get; set; }

        public DateTime BiddingDate { get; set; }
        public ICollection<BiddingTransaction> BiddingTransactions { get; set; }
    }
}
