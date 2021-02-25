using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MVCWebAppServierCon.Models
{
    public class OrderTypeClass
    {
        [Key]
        public int orderTypeCode { get; set; }
        [Required]
        public String orderTypeName { get; set; }
        [Required]
        public int orderTypePeriodInDays { get; set; }
        [Required]
        public int orderTypeUserId { get; set; }
        [Required]
        public DateTime orderTypeCreationDate { get; set; }

        public String orderTypeNote { get; set; }

        public Boolean orderTypeShowFunder { get; set; }

        public Boolean orderTypeShowBudgetLine { get; set; }
        public int orderTypeNumDays { get; set; }
        public int? orderTypeNumDaysAfter { get; set; }
        public Boolean orderTypeShowTransDate { get; set; }

    }
}
