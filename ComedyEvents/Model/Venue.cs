using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComedyEvents.Model
{
    public class Venue
    {
        public int VenueID { get; set; }
        public string VenueName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public int Seating { get; set; }
        public bool ServesAlcohol { get; set; }
    }
}
