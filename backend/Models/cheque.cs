using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace backend.Models
{
    public class cheque
    {
        [Key]
        public int cno { get; set; }
        public int accno { get; set; }
        public string status { get; set; }
        public int amount { get; set; }
    }
}
