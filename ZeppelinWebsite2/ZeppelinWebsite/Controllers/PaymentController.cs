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
    public class PaymentController : Controller
    {

        private ZeppelinnDBEntities objzeppelinDBEntities;

        public PaymentController()
        {

            objzeppelinDBEntities = new ZeppelinnDBEntities();

        }

        
        public ActionResult ak()
        {

            return View();
        }

        // GET: Payment
        public ActionResult Index()
        {
            PaymentViewModel objPaymentViewModel = new PaymentViewModel();

            objPaymentViewModel.ListOfPaymentType = (from obj in objzeppelinDBEntities.PaymentTypes
                                                    
                                                    select new SelectListItem()
                                                   {
                                                       Text = obj.PaymentTypeName,
                                                       Value = obj.PaymentTypeId.ToString()

                                                   }).ToList();


            return View(objPaymentViewModel);
        }

        [HttpPost]
        public ActionResult Index(PaymentViewModel objPaymentViewModel)
        {

            string message = String.Empty;
            string ImageUniqueName = String.Empty;
            string ActualImageName = String.Empty;
            if (objPaymentViewModel.PaymentId == 0)
            {

                ImageUniqueName = Guid.NewGuid().ToString();
               
               
                //objFlightDbEntities
                Payment objPayment = new Payment()
                {
                    PaymentType= objPaymentViewModel.PaymentType,
                    PaymentAmount=objPaymentViewModel.PaymentAmount,
                    IsActive=true
                };
                //objzeppelinDBEntities.SaveChanges();
                objzeppelinDBEntities.Payments.Add(objPayment);
               
                message = "Added.";
            }
            else
            {
                Payment objPayment = objzeppelinDBEntities.Payments.Single(model => model.PaymentId == objPaymentViewModel.PaymentId);

                objPayment.PaymentType = objPaymentViewModel.PaymentType;
                objPayment.PaymentAmount = objPaymentViewModel.PaymentAmount;
                objPayment.IsActive = true;



               
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
            return Json(data: new { message = "Zeppline Successfully " + message, success = true }, JsonRequestBehavior.AllowGet);
        }


       

    }
}