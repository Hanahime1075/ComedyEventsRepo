using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComedyEvents.DTO
{
    public class ComedianDTO
    {
        [Required]
        public int ComedianID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        public string ContactPhone { get; set; }
    }
}
