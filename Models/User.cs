using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace wedding.Models
{
    public class User : BaseEntity
    {
        public int UserId { get; set; }

        public string First { get; set; }
        
        public string Last { get; set; }
       
        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    
        public List<Guest> Weddings { get; set; }
        public User()
        {
            Weddings = new List<Guest>();
        }
    }
}
