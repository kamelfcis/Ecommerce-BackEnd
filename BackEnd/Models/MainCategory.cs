using System.Collections.Generic;

namespace BackEnd.Models
{
    public class MainCategory
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public ICollection<Category> Categories { get; set; }
    }
}
