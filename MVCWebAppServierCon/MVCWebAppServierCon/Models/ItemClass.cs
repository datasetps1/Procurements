using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MVCWebAppServierCon.Models
{
    public class ItemClass
    {
        [Key]
        public int itemCode { get; set; }
        [Required]
        public String itemName { get; set; }
        [Required]
        public String itemName2 { get; set; }
        [Required]
        public float itemPrice { get; set; }
        [Required]
        public int itemUserId { get; set; }
        [Required]
        public DateTime itemCreationDate { get; set; }
        public String itemNote { get; set; }
    }
}
