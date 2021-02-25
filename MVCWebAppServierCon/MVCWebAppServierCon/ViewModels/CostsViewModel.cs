using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebAppServierCon.ViewModels
{
    public class CostsViewModel
    {
        [Required]
        public string costCode { get; set; }
        [Required]
        public String costName { get; set; }
        [Required]
        public float? costBudget { get; set; }
    }


    public class CodeNameModel
    {

        public string Code { get; set; }

        public String Name { get; set; }

    }
}
