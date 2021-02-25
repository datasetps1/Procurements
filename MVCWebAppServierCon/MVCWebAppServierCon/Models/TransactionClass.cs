using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCWebAppServierCon.Models
{
    public class TransactionClass
    {
        [Key]
        public int TransactionCode { get; set; }
        [Required]
        public int TransactionOrderHeaderCode { get; set; }
        [Required]
        public int TransactionItemCode { get; set; }
        [Required]
        public double TransactionQty { get; set; }
        [Required]
        public float TransactionPrice { get; set; }

        public String TransactionNote { get; set; }

        [Required]
        public string TransactionUserId { get; set; }
        [Required]
        public DateTime TransactionCreationDate { get; set; }
        [Required]
        public int TransactionDeviceIp { get; set; }
        public DateTime TransactionDate { get; set; }
        [NotMapped]
        public String TransactionItemName { get; set; }

    }
}
