namespace skimmer2.Models
{
    public class User
    {
        public int Id { get; set; } // Primary key
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } // Consider hashing in a real app
    }
}