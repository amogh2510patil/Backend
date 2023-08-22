namespace backend.Models
{
    public class User
    {
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = "User";
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
