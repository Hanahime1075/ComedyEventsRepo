using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComedyEvents.Model
{
    public class Gig
    {
        public int GigID { get; set; }
        public string GigHeadline { get; set; }
        public int GigLengthInMinutes { get; set; }
        public Event Event { get; set; }
        public Comedian Comedian { get; set; }
    }
}
