using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.ApiClient.Models
{
    [Table("Employees")]
    public class Employee
    {
        [Column("EmployeeID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Employee ID")]
        [Required(ErrorMessage = "Employee ID is required")]        
        public int EmployeeID { get; set; }

        [Column("FirstName")]
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is required")]
        [StringLength(20,ErrorMessage ="First name must be less than 20 characters")]       
        public string FirstName { get; set; }

        [Column("LastName")]
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(30, ErrorMessage = "Last name must be less than 30 characters")]        
        public string LastName { get; set; }

        [Column("Title")]
        [Display(Name = "title")]
        [Required(ErrorMessage = "Title is required")]
        [StringLength(30, ErrorMessage = "Title must be less than 30 characters")]       
        public string Title { get; set; }

        [Column("BirthDate")]
        [Display(Name = "Birth Date")]
        [Required(ErrorMessage = "Birth Date is required")]
        public DateTime BirthDate { get; set; }

        [Column("HireDate")]
        [Display(Name = "Hire Date")]
        [Required(ErrorMessage = "Hire Date is required")]
        public DateTime HireDate { get; set; }

        [Column("Country")]
        [Display(Name = "Country")]
        [Required(ErrorMessage = "Country is required")]
        [StringLength(30, ErrorMessage = "Country must be less than 30 characters")]             
        public string Country { get; set; }

        [Column("Notes")]
        [Display(Name = "Notes")]
        [StringLength(30, ErrorMessage = "Notes must be less than 500 characters")]        
        public string Notes { get; set; }
    }
}
