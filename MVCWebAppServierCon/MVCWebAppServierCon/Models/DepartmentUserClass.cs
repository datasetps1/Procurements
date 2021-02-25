using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebAppServierCon.Models
{
    public class DepartmentUserClass
    {
        [Key]
        public int departmentCode { get; set; }
        [Required]

        public String departmentName { get; set; }
        [StringLength(450)]
        public String UserGeneralManagerName { get; set; }
        [StringLength(450)]
        public String UserManagerName { get; set; }
        [StringLength(450)]
        public String UserHeadName { get; set; }
        [StringLength(450)]
        public String UserFinancialName { get; set; }
        [StringLength(450)]
        public String UserProcurementSectionName { get; set; }

        public String departmentNote { get; set; }

    }
}
