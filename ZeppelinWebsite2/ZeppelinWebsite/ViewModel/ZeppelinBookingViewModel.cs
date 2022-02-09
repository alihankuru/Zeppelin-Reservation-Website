using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZeppelinWebsite.ViewModel
{
    public class ZeppelinBookingViewModel
    {

        public int BookingId { get; set; }

        public string CustomerName { get; set; }

        public int NumberOfMembers { get; set; }

        public string CustomerAddress { get; set; }

        public string CustomerPhone { get; set; }

        public DateTime BookingFrom { get; set; }


        public string ZeppelinNumber { get; set; }

        public decimal ZeppelinPrice { get; set; }

        public decimal TotalAmount { get; set; }

    }
}