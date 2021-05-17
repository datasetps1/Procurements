using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MVCWebAppServierCon.Models
{
    public class ApprovalClass
    {
        [Key]
        public int ApprovalCode { get; set; }
        [Required]
        public int ApprovalHeaderCode { get; set; }
        //[Required]
        //public int ApprovalEmployeeCode { get; set; }
        [Required]
        public int ApprovalIsApproved { get; set; } // approval statuscdoe// 0:notApproved or 1:Approved  ///

        public String ApprovalNote { get; set; }

        [Required]
        public String ApprovalUserId { get; set; }  // we will use it as from User

        [Required]
        public DateTime ApprovalCreationDate { get; set; }
        [Required]
        public int ApprovalDeviceIp { get; set; }
        [Required]
        public String ToUser { get; set; }


    }
}
