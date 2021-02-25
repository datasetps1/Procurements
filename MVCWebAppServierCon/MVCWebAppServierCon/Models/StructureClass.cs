using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace MVCWebAppServierCon.Models
{
    public class StructureClass
    {
        [Key]
        public int structureCode { get; set; }
        [Required]
        public String structureName { get; set; }
        [Required]
        public String structureName2 { get; set; }
        [Required]
        public int structureRank { get; set; }
        [Required]
        public int structureUserId { get; set; }
        [Required]
        public DateTime structureCreationDate { get; set; }
        public String structureNote { get; set; }

    }
}
