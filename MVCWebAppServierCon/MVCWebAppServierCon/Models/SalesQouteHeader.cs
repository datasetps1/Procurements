using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebAppServierCon.Models
{
    public class SalesQouteHeader
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name= "Offer Name")]
        public string OfferName { get; set; }

        [Required]
        [Display(Name = "Offer Date")]
        public DateTime OfferDate { get; set; }

        [Required]
        [Display(Name = "Expier Date")]
        public DateTime ExpierDate { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
        
        public ICollection<SalesSuppliers> salesSuppliers { get; set; }

        public ICollection<SalesCriterias> salesCriterias { get; set; }
        
        [ForeignKey("UserClass")]
        public string userCode { get; set; }
        public UserClass User { get; set; }

        public string Description { get; set; }

    }
}
