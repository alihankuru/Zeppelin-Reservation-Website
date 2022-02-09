using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeppelinWebsite.Filters;
using ZeppelinWebsite.Models;
using ZeppelinWebsite.ViewModel;

namespace ZeppelinWebsite.Controllers
{
    public class FlightController : Controller
    {
        private ZeppelinnDBEntities objzeppelinDBEntities;

        public FlightController()
        {

            objzeppelinDBEntities = new ZeppelinnDBEntities();

        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Index()
        {
            ZeppelinViewModel objZeppelinViewModel = new ZeppelinViewModel();
            objZeppelinViewModel.ListOfBookingStatus = (from obj in objzeppelinDBEntities.BookingStatus
                                                        select new SelectListItem()
                                                        {
                                                            Text=obj.BookingStatus,
                                                            Value = obj.BookingStatusId.ToString()

                                                        }).ToList();


            objZeppelinViewModel.ListOfSeatType = (from obj in objzeppelinDBEntities.SeatTypes
                                                   select new SelectListItem()
                                                   {
                                                       Text = obj.SeatTypeName,
                                                       Value = obj.SeatTypeId.ToString()

                                                   }).ToList();
            return View(objZeppelinViewModel);
        }

        [HttpPost]
        public ActionResult Index(ZeppelinViewModel objZeppelinViewModel)
        {

            string message = String.Empty;
            string ImageUniqueName = String.Empty;
            string ActualImageName = String.Empty;
            if (objZeppelinViewModel.ZeppelinId == 0)
            {

                ImageUniqueName = Guid.NewGuid().ToString();
                ActualImageName = ImageUniqueName + Path.GetExtension(objZeppelinViewModel.Image.FileName);
                objZeppelinViewModel.Image.SaveAs(Server.MapPath("~/ZeppelinImages/" + ActualImageName));
                //objFlightDbEntities
                Zeppelin objZeppelin = new Zeppelin()
                {
                    ZeppelinNumber = objZeppelinViewModel.ZeppelinNumber,
                    ZeppelinFrom = objZeppelinViewModel.ZeppelinFrom,
                    ZeppelinTo = objZeppelinViewModel.ZeppelinTo,
                    ZeppelinPrice = objZeppelinViewModel.ZeppelinPrice,
                    BookingStatusId = objZeppelinViewModel.BookingStatusId,
                    IsActive = true,
                    ZeppelinImage = ActualImageName,
                    ZeppelinCapacity = objZeppelinViewModel.ZeppelinCapacity,
                    SeatTypeId = objZeppelinViewModel.SeatTypeId
                };
                //objzeppelinDBEntities.SaveChanges();
                objzeppelinDBEntities.Zeppelins.Add(objZeppelin);
                message = "Added.";
            }
            else
            {
                Zeppelin objZeppelin = objzeppelinDBEntities.Zeppelins.Single(model => model.ZeppelinId == objZeppelinViewModel.ZeppelinId);

                if (objZeppelinViewModel.Image != null)
                {
                     ImageUniqueName = Guid.NewGuid().ToString();
                     ActualImageName = ImageUniqueName + Path.GetExtension(objZeppelinViewModel.Image.FileName);

                    objZeppelinViewModel.Image.SaveAs(Server.MapPath("~/ZeppelinImages/" + ActualImageName));
                    objZeppelin.ZeppelinImage = ActualImageName;

                }



                objZeppelin.ZeppelinNumber = objZeppelinViewModel.ZeppelinNumber;
                objZeppelin.ZeppelinFrom = objZeppelinViewModel.ZeppelinFrom;
                objZeppelin.ZeppelinTo= objZeppelinViewModel.ZeppelinTo;
                objZeppelin.ZeppelinPrice = objZeppelinViewModel.ZeppelinPrice;
                objZeppelin.BookingStatusId = objZeppelinViewModel.BookingStatusId;
                objZeppelin.IsActive = true;
               
                objZeppelin.ZeppelinCapacity = objZeppelinViewModel.ZeppelinCapacity;
                objZeppelin.SeatTypeId = objZeppelinViewModel.SeatTypeId;
                message = "Updated.";
            }
            
            try
            {
                objzeppelinDBEntities.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                Console.WriteLine(e);
            }
            return Json(data: new { message = "Zeppline Successfully "+message, success = true }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetAllZeppelin()
        {


            IEnumerable<ZeppelinDetailsViewModel> listOfZeppelinDetailsViewModels =
                (from objZeppelin in objzeppelinDBEntities.Zeppelins
                 join objBooking in objzeppelinDBEntities.BookingStatus on objZeppelin.BookingStatusId equals objBooking.BookingStatusId
                 join objSeatType in objzeppelinDBEntities.SeatTypes on objZeppelin.SeatTypeId equals objSeatType.SeatTypeId
                 where objZeppelin.IsActive == true 
                 select new ZeppelinDetailsViewModel() {
                     ZeppelinNumber=objZeppelin.ZeppelinNumber,
                     
                     ZeppelinFrom= objZeppelin.ZeppelinFrom,
                     ZeppelinTo=objZeppelin.ZeppelinTo,
                     ZeppelinCapacity =objZeppelin.ZeppelinCapacity,
                     ZeppelinPrice=objZeppelin.ZeppelinPrice,
                     BookingStatus=objBooking.BookingStatus,
                     SeatType= objSeatType.SeatTypeName,
                     ZeppelinImage=objZeppelin.ZeppelinImage,
                     ZeppelinId=objZeppelin.ZeppelinId
                 }).ToList();



            return PartialView("_ZeppelinDetailsPartial", listOfZeppelinDetailsViewModels);
        }


        [HttpGet]
        public JsonResult EditZeppelinDetails(int zeppelinId)
        {
            var result = objzeppelinDBEntities.Zeppelins.Single(model => model.ZeppelinId == zeppelinId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DeleteZeppelinDetails(int zeppelinId)
        {
            Zeppelin objZeppelin = objzeppelinDBEntities.Zeppelins.Single(model => model.ZeppelinId == zeppelinId);
            objZeppelin.IsActive = false;
            objzeppelinDBEntities.SaveChanges();

            return Json(data: new { message = "Record Successfully Deleted" , success= true}, JsonRequestBehavior.AllowGet);

        }


    }
    

}
