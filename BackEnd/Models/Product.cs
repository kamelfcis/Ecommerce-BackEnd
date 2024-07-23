namespace BackEnd.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public byte[]? Image { get; set; }



        public int Rate { get; set; }
        public string ImageStr { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
         

        
        public bool OnBidding { get; set; }
        public bool InStock { get; set; }

        public bool Featured { get; set; }
        public bool BestSeller { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }


    }
}
