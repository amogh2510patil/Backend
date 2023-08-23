using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class User
    {
        [Key]
        public string Username { get; set; } = string.Empty;
        public int accNo { get; set; } = 0;
        public string Role { get; set; } = "User";
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Status { get; set; } = "Activate";
    }
}
