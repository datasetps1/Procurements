using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MVCWebAppServierCon.Models
{
    public class UserClass
    {
        [Key]
        public string userCode { get; set; }
        [Required]
        public String userName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public String userEmail { get; set; }
        [Required]
        public int userTypeCode { get; set; }
        public int userDepartmentCode { get; set; }
        [Required]
        public int userActive { get; set; }
        [Required]
        public string userUserId { get; set; }
        [Required]
        public DateTime userCreationDate { get; set; }
        public String userNote { get; set; }
    }
}
