using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Via.Models
{
    public class Picture
    {
        public int PictureId { get; set; }
        public int AttendeeId { get; set; }
        public string Url { get; set; }

        public Attendee Attendee { get; set; }

    }
}
