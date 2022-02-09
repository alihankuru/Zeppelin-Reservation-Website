using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZeppelinWebsite.ViewModel
{
    public class ZeppelinDetailsViewModel
    {
        public int ZeppelinId { get; set; }

        public string ZeppelinNumber { get; set; }

        public string ZeppelinImage { get; set; }

        public decimal ZeppelinPrice { get; set; }

        public string BookingStatus { get; set; }

        public string SeatType { get; set; }

        public int ZeppelinCapacity { get; set; }

        //public string ZeppelinDescription { get; set; }

        public string ZeppelinFrom{ get; set; }

        public string ZeppelinTo { get; set; }

    }
}