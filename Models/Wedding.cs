using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace wedding.Models
{
    public class Wedding : BaseEntity
    {
        public int WeddingId { get; set; }
        public string Wedder1 { get; set; }

        public string Wedder2 { get; set; }
        
        public DateTime Date { get; set; }
       
        public string Address { get; set; }


        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    
        public int Creator { get; set; }
        public List<Guest> Guests { get; set; }

        public Wedding(){
            Guests = new List<Guest>();
        }
    }
}
