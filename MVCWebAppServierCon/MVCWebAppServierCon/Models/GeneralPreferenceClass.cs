using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebAppServierCon.Models
{
    public class GeneralPreferenceClass
    {

        [Key]
        public int code { get; set; }
        [Required]
        [Display(Name= "Qoute Amount")]
        public float QouteAmount { get; set; }
        [Display(Name = "Deduction Amount")]
        public float DeductionAmount { get; set; }
        [Display(Name = "Connect With")]
        public string ConnecWith { get; set; }
        [Display(Name = "Project Table")]
        public string ProjectTable { get; set; }
        [Display(Name = "Activitiy Table")]
        public string ActivitiyTable { get; set; }
    }
}
