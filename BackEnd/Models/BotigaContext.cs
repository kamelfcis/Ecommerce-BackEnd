using Microsoft.EntityFrameworkCore;

namespace BackEnd.Models
{
    public partial class BotigaContext : DbContext
    {
        public BotigaContext(DbContextOptions options) : base(options)
        {
        }
        
        public DbSet<User> Users { get; set; } 
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<MainCategory> MainCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }  
        public DbSet<BidItem> BidItems { get; set; }
        public DbSet<BiddingTransaction> BiddingTransaction { get; set; }

    }

}
