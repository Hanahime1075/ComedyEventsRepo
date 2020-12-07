using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComedyEvents.DTO
{
    public class GigDTO
    {
        [Required]
        public int GigID { get; set; }

        [Required]
        [StringLength(20)]
        public string GigHeadline { get; set; }

        [Range(15,120)]
        public int GigLengthInMinutes { get; set; }
        public EventDTO Event { get; set; }
        public ComedianDTO Comedian { get; set; }
    }
}
