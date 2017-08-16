using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace wedding.Models
{
    public class RegUser : BaseEntity
    {
      
        [Required(ErrorMessage = "First Name Required")]
        [MinLength(2, ErrorMessage = "First name not long enough")]
        [Display(Name = "First Name")]
        public string First { get; set; }
        
        [Required(ErrorMessage = "Last Name Required")]
        [MinLength(2, ErrorMessage = "Last name not long enough")]
        [Display(Name = "Last Name")]
        public string Last { get; set; }
       
        [Required(ErrorMessage = "Email Required")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Password Required")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password Required")]
        [Display(Name = "Confirm Password")]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}