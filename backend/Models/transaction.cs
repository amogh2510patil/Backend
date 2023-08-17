using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class transaction
    {
        [Key]
        public int transactionNo { get; set; }

        public int amount { get; set; }
        public string type { get; set; }
        public DateTime dateTime { get; set; }

        [ForeignKey("customer")]
        public int accountnum { get; set; }
        
        public int? recipient { get; set; }

        //public virtual customer? customer { get; set; }
    }
}
