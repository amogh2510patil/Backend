using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class customer
    {

        [Required(ErrorMessage = "Pls Enter Name")]
        public string name { get; set; }
        [Required(ErrorMessage = "Pls Enter Address")]
        public string address { get; set; }
        [Required(ErrorMessage = "Pls Enter Email Address")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string email { get; set; }
        [RegularExpression("[6-9][0-9]{9}", ErrorMessage = "Invalid Mobile no")]
        ///[Phone]
        public string contact { get; set; }
        [Required]
        [RegularExpression("[1-9][0-9]{7}", ErrorMessage = "Pls enter correct cardnumber")]
        public int cardnumber { get; set; }
        [Required]
        [RegularExpression("[0-9][0-9]{3}", ErrorMessage = "Pls enter correct pin")]
        public int pinnum { get; set; }
        [Required(ErrorMessage = "Pls enter city name")]
        public string city { get; set; }
        [Required(ErrorMessage = "Pls enter account type number 1 or 2")]
        public int accounttype { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        //[MinLength(8)]
        //[RegularExpression("[0-9][0-9]{7}",ErrorMessage ="Pls enter the 12-digit account number")]
        public int accountnum { get; set; }
        [Required(ErrorMessage = "Invalid Amount")]
        public double balance { get; set; }

        //public virtual List<transaction>? transactions { get; set; }

    }
}
