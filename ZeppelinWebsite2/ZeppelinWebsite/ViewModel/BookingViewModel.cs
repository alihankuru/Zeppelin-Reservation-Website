using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ZeppelinWebsite.ViewModel
{
    public class BookingViewModel
    {
        [Display(Name = "Customer Name")]
        [Required(ErrorMessage ="Customer name is required")]
        public string CustomerName { get; set; }

        [Display(Name = "Customer Address")]
        [Required(ErrorMessage = "Customer Address is required")]
        public string CustomerAddress { get; set; }

        [Display(Name = "Customer Phone")]
        [Required(ErrorMessage = "Customer Phone is required")]
        public string CustomerPhone { get; set; }


        [Display(Name = "Booking From")]
        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd-MMM-yyyy}",ApplyFormatInEditMode =true)]
        [Required(ErrorMessage = "Booking From is required")]
        public DateTime BookingFrom { get; set; }

        //[Display(Name = "Booking To")]
        ////[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        //[Required(ErrorMessage = "Booking To is required")]
        //public DateTime BookingTo { get; set; }

        [Display(Name = "Assign Zeppelin")]
        [Required(ErrorMessage = "Zeppelin is required")]
        public int AssignZeppelinId { get; set; }

        [Display(Name = "Number Of Member")]
        [Required(ErrorMessage = "Number Of member is required")]
        public int NumberOfMembers { get; set; }

        public IEnumerable<SelectListItem> ListOfZeppelins {get; set;}

    }
}