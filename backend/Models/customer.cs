using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class customer
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string address { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string contact { get; set; }
        [Required]
        public int cardnumber { get; set; }
        [Required]
        public int pinnum { get; set; }
        [Required]
        public string city { get; set; }
        [Required]
        public bool accounttype { get; set; }

        [Key]
        [Required]
        public int accountnum { get; set; }
        [Required]
        public int balance { get; set; }

    }
}
