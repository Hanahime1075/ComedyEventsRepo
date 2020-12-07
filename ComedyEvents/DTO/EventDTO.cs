using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComedyEvents.DTO
{
    public class EventDTO
    {
        [Required]
        public int EventID { get; set; }

        [Required]
        [StringLength(20)]
        public string EventName { get; set; }

        public DateTime EventDate { get; set; }
        public VenueDTO Venue { get; set; }
        public ICollection<GigDTO> Gigs { get; set; }
    }
}
