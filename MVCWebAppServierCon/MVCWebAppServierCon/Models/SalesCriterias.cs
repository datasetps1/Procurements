using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebAppServierCon.Models
{
    public class SalesCriterias
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CriteriaId { get; set; }

        [Required]
        public int Percentage { get; set; }

        public int salesQouteHeaderId { get; set; }
        //public SalesQouteHeader salesQouteHeader { get; set; }
    }
}
