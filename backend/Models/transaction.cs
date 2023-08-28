using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int transactionNo { get; set; }

        [Required]
        public double amount { get; set; }
        [Required(ErrorMessage="Pls enter fund transfer type")]
        public string type { get; set; }
        public DateTime dateTime { get; set; }

        [Required(ErrorMessage ="Pls enter the currency type")]
        public string currency { get; set; }

        [ForeignKey("customer")]
        public int accountnum { get; set; }
        
        public int? recipient { get; set; }

        //public virtual customer? customer { get; set; }
    }
}
