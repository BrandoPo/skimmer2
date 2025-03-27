namespace skimmer2.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Username { get; set; } // Nullable string
        public string? Email { get; set; }    // Nullable string
        public string? Password { get; set; } // Nullable string
    }
}