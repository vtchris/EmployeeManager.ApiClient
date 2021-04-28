using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.ApiClient.Models
{
    public class Register
    {
        [Display(Name = "User Name")]
        [Required]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Email")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Full Name")]
        [Required]
        public string FullName { get; set; }

        [Display(Name = "Birth Date")]
        [Required]
        public DateTime BirthDate { get; set; }
    }
}
