using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.ApiClient.Models
{
    public class SignIn
    {
        [Display(Name = "User Name")]
        [Required]       
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        [Required]
        public bool RememberMe { get; set; }

    }
}
