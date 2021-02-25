using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebAppServierCon.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public String Name { get; set; }
        [Required]
        [EmailAddress]
        public String Emial { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public String Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="The Passwords don't matches")]
        public String ConfirmPassword { get; set; }
    }
}
