namespace BackEnd.DTO.Auth
{
    public class Register
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public int RoleId { get; set; }
    }
}
