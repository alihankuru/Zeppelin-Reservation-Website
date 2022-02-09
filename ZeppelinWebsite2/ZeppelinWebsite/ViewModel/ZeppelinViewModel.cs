using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ZeppelinWebsite.ViewModel
{
    public class ZeppelinViewModel
    {
        [Key]
        public int ZeppelinId { get; set; }

        [Display(Name ="Zeppelin Number")]
        [Required(ErrorMessage ="Zeppelin Number is required.")]
        public string ZeppelinNumber { get; set; }

        [Display(Name = "Zeppelin Image")]
        public string ZeppelinImage { get; set; }

        [Display(Name = "Zeppelin Price")]
        [Required(ErrorMessage = "Zeppelin Price is required.")]
        [Range(1, 10000, ErrorMessage = "Zeppelin price should be equal and greater than {1}")]
        public decimal ZeppelinPrice { get; set; }

        [Display(Name = "Booking Status")]
        [Required(ErrorMessage = "Booking status is required.")]
        public int BookingStatusId { get; set; }

        [Display(Name = "Seat Type")]
        [Required(ErrorMessage = "Seat Type is required.")]
        public int SeatTypeId { get; set; }

        [Display(Name = "Zeppelin Capacity")]
        [Required(ErrorMessage = "Zeppelin Capacity is required.")]
        [Range(1,20, ErrorMessage ="Zeppelin Capacity should be equal and greater than {1}")]
        public int ZeppelinCapacity { get; set; }

        public HttpPostedFileBase Image { get; set; }

        [Display(Name = "Zeppelin From")]
        [Required(ErrorMessage = "Zeppelin From is required.")]
        public string ZeppelinFrom { get; set; }

        [Display(Name = "Zeppelin To")]
        [Required(ErrorMessage = "Zeppelin To is required.")]
        public string ZeppelinTo { get; set; }


        public List<SelectListItem> ListOfBookingStatus { get; set; }

        public List<SelectListItem> ListOfSeatType { get; set; }
    }
}