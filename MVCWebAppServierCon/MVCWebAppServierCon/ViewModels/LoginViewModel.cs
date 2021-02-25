using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Localization;




namespace MVCWebAppServierCon.ViewModels
{
    public class LoginViewModel
    {
        /*[EmailAddress]
        */

        [Required]
        public String Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }

        public string Name { get; set; }

        public String LogoExt { get; set; }

        public byte[] LogoPath { get; set; }

        public string logosting { get; set; }
    }
}
