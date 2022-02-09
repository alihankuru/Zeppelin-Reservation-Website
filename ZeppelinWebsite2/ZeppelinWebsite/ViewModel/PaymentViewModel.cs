using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZeppelinWebsite.ViewModel
{
    public class PaymentViewModel
    {
        public int PaymentId{ get; set; }

        public int BookingId { get; set; }

        public int PaymentType { get; set; }

        public decimal PaymentAmount{ get; set; }

        public List<SelectListItem> ListOfPaymentType { get; set; }

    }
}