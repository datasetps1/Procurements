using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCWebAppServierCon.Models
{
    public class AmountSittingClass
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int amountCode { get; set; }
        [Required]
        public int amountFrom { get; set; }
        [Required]
        public int amountTo { get; set; }
        [Required]
        public int amountStructure { get; set; }
        [Required]
        public string amountUserId { get; set; }
        [Required]
        public DateTime amountCreationDate { get; set; }
        public String amountNote { get; set; }
    }
}
