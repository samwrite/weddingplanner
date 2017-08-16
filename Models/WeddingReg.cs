using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace wedding.Models
{
    public class WeddingReg : BaseEntity
    {
      
        [Required(ErrorMessage = "Wedder One Required")]
        [Display(Name = "Wedder One")]
        public string Wedder1 { get; set; }
        
        [Required(ErrorMessage = "Wedder Two Required")]
        [Display(Name = "Wedder Two")]
        public string Wedder2 { get; set; }
       
        [Required(ErrorMessage = "Date Required")]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }
        
        [Required(ErrorMessage = "Address Required")]
        [Display(Name = "Address")]
        public string Address { get; set; }
    }
}