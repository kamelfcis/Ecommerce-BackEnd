﻿namespace BackEnd.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
    }

}