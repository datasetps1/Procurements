using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebAppServierCon.Models
{
    public class OrderAnalysis
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int SalesQouteHeaderId { get; set; }
        [Required]
        public int SalesCriteriasId { get; set; }
        [Required]
        public int SalesSuppliersId { get; set; }
        [Required]
        public Double Statement { get; set; }
        [Required]
        public Double Percentage { get; set; }
    }
}
