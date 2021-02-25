using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MVCWebAppServierCon.Models
{
    public class DepartmentClass
    { // check if these sections prop should be Id/Code if so make them int type
        [Key]
        public int departmentCode { get; set; }
        [Required]

        public String departmentName { get; set; }
        [StringLength(450)]
        public String departmentGeneralManagerCode { get; set; }
        [StringLength(450)]
        public String departmentManagerCode { get; set; }
        [StringLength(450)]
        public String departmentHeadCode { get; set; }
        [StringLength(450)]
        public String departmentFinancialCode { get; set; }
        [StringLength(450)]
        public String departmentProcurementSectionCode { get; set; }
      
        public String departmentUserId { get; set; }
        public int departmentDeviceIp { get; set; }
        [Required]
        public DateTime departmentCreationDate { get; set; }
        public String departmentNote { get; set; }

       
    }
}
