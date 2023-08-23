namespace backend.Models
{
    public class UserDto
    {
        public string Username { get; set; } = string.Empty;
        public int accNo { get; set; } = 0;
        public string Role { get; set; } = "User";
        public string Password { get; set; } = string.Empty;
    }
}
