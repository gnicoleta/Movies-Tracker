using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class UserViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [EmailAddress]
        public String Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least " + "{2} chars long", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public String Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The pass and confirmation" + "pass does not match")]
        public String ConfirmPassword {get; set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }

}
}