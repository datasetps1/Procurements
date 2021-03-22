using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebAppServierCon.ViewModels
{
    public class UserViewModel
    {
        [Required]
        public String userName { get; set; }
        [Required]
        public String userEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public String userPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("userPassword", ErrorMessage = "Passwords doesn't match.")]
        public String userConfirmPassword { get; set; }
        [Required]
        public int userType { get; set; }
        public int userDepartment { get; set; }
        [Required]
        public int userActive { get; set; }
        public String userNote { get; set; }

        public string userCode { get; set; }

        public string Excutable { get; set; }

    }
}
