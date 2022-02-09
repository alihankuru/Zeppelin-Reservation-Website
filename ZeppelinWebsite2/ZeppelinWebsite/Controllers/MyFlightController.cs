using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeppelinWebsite.Models;
using ZeppelinWebsite.ViewModel;

namespace ZeppelinWebsite.Controllers
{
    public class MyFlightController : Controller
    {
        private ZeppelinnDBEntities objzeppelinDBEntities;

        public MyFlightController()
        {
            objzeppelinDBEntities = new ZeppelinnDBEntities();

        }


        public ActionResult Search(string searching)
        {
            var bookings = from s in objzeppelinDBEntities.Zeppelins
                           select s;

            if (!String.IsNullOrEmpty(searching))
            {
                bookings = bookings.Where(s => s.ZeppelinFrom.Contains(searching));
            }

            return View(bookings.ToList());
        }


        // GET: Booking
        public ActionResult Index()
        {
            BookingViewModel objBookingViewModel = new BookingViewModel();
            objBookingViewModel.ListOfZeppelins = (from objZeppelin in objzeppelinDBEntities.Zeppelins
                                                   where objZeppelin.BookingStatusId == 2
                                                   select new SelectListItem()
                                                   {
                                                       Text = objZeppelin.ZeppelinNumber,
                                                       Value = objZeppelin.ZeppelinId.ToString()
                                                   }
                                                 ).ToList();
            objBookingViewModel.BookingFrom = DateTime.Now;
            //objBookingViewModel.BookingTo = DateTime.Now.AddDays(1);
            return View(objBookingViewModel);
        }



        [HttpPost]
        public ActionResult Index(BookingViewModel objBookingViewModel)
        {
            //int numberOfDays = Convert.ToInt32((objBookingViewModel.BookingTo - objBookingViewModel.BookingFrom).TotalDays);
            Zeppelin objZeppelin = objzeppelinDBEntities.Zeppelins.Single(model => model.ZeppelinId == objBookingViewModel.AssignZeppelinId);
            decimal ZeppelinPrice = objZeppelin.ZeppelinPrice;
            decimal TotalAmount = ZeppelinPrice;

            ZeppelinBooking zeppelinBooking = new ZeppelinBooking()
            {
                BookingFrom = objBookingViewModel.BookingFrom,
                AssignZeppelinId = objBookingViewModel.AssignZeppelinId,
                CustomerAddress = objBookingViewModel.CustomerAddress,
                CustomerName = objBookingViewModel.CustomerName,
                CustomerPhone = objBookingViewModel.CustomerPhone,


                NoOfMembers = objBookingViewModel.NumberOfMembers,
                TotalAmount = TotalAmount


            };

            objzeppelinDBEntities.ZeppelinBookings.Add(zeppelinBooking);
            try
            {
                objzeppelinDBEntities.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                Console.WriteLine(e);
            }


            //objzeppelinDBEntities.SaveChanges();

            objZeppelin.BookingStatusId = 3;

            objzeppelinDBEntities.SaveChanges();
            //string dt = objBookingViewModel.BookingTo.ToString(format: "MM/dd/yyyy");
            return Json(data: new { message = "Zeppelin Booking is succussfully Created.", success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public PartialViewResult GetAllBookingHistory()
        {
            List<ZeppelinBookingViewModel> listOfZeppelinBookingViewModels = new List<ZeppelinBookingViewModel>();
            listOfZeppelinBookingViewModels = (from objZeppelinBooking in objzeppelinDBEntities.ZeppelinBookings
                                               join objZeppelin in objzeppelinDBEntities.Zeppelins on objZeppelinBooking.AssignZeppelinId equals objZeppelin.ZeppelinId
                                               select new ZeppelinBookingViewModel()
                                               {
                                                   BookingFrom = objZeppelinBooking.BookingFrom,
                                                   //BookingTo=objZeppelinBooking.BookingTo,
                                                   CustomerPhone = objZeppelinBooking.CustomerPhone,
                                                   CustomerName = objZeppelinBooking.CustomerName,
                                                   TotalAmount = objZeppelinBooking.TotalAmount,
                                                   CustomerAddress = objZeppelinBooking.CustomerAddress,
                                                   NumberOfMembers = objZeppelinBooking.NoOfMembers,
                                                   BookingId = objZeppelinBooking.BookingId,
                                                   ZeppelinNumber = objZeppelin.ZeppelinNumber,
                                                   ZeppelinPrice = objZeppelin.ZeppelinPrice

                                               }).ToList();
            return PartialView("_BookingHistoryPartial", listOfZeppelinBookingViewModels);
        }


        public PartialViewResult GetAllZeppelin()
        {


            IEnumerable<ZeppelinDetailsViewModel> listOfZeppelinDetailsViewModels =
                (from objZeppelin in objzeppelinDBEntities.Zeppelins
                 join objBooking in objzeppelinDBEntities.BookingStatus on objZeppelin.BookingStatusId equals objBooking.BookingStatusId
                 join objSeatType in objzeppelinDBEntities.SeatTypes on objZeppelin.SeatTypeId equals objSeatType.SeatTypeId
                 where objZeppelin.IsActive == true
                 select new ZeppelinDetailsViewModel()
                 {
                     ZeppelinNumber = objZeppelin.ZeppelinNumber,

                     ZeppelinFrom = objZeppelin.ZeppelinFrom,
                     ZeppelinTo = objZeppelin.ZeppelinTo,
                     ZeppelinCapacity = objZeppelin.ZeppelinCapacity,
                     ZeppelinPrice = objZeppelin.ZeppelinPrice,
                     BookingStatus = objBooking.BookingStatus,
                     SeatType = objSeatType.SeatTypeName,
                     ZeppelinImage = objZeppelin.ZeppelinImage,
                     ZeppelinId = objZeppelin.ZeppelinId
                 }).ToList();



            return PartialView("_ZeppelinDetailsPartial", listOfZeppelinDetailsViewModels);
        }

    }
}