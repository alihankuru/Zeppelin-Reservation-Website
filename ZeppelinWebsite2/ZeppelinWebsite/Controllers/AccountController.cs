using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ZeppelinWebsite.Helper;
using ZeppelinWebsite.Models;
using ZeppelinWebsite.ViewModel;

namespace ZeppelinWebsite.Controllers
{
    public class AccountController : Controller
    {

        private ZeppelinnDBEntities objzeppelinDBEntities;

        public AccountController()
        {
            objzeppelinDBEntities = new ZeppelinnDBEntities();


        }



        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel objLoginViewModel)
        {
            if (!objzeppelinDBEntities.Users.Any(model => model.Email == objLoginViewModel.Email))
            {
                ModelState.AddModelError(key:"", errorMessage:"Email is not valid.");
                return View(objLoginViewModel);
            }

            User objUser = objzeppelinDBEntities.Users.FirstOrDefault(model => model.Email == objLoginViewModel.Email);

            if (!objUser.IsVerified)
            {
                ModelState.AddModelError(key: "", errorMessage: "Email is not verified, Please check link and confirm.");
                return View(objLoginViewModel);
            }

            byte[] salt = Convert.FromBase64String( objUser.UserSalt);
            byte[] bytePassword = Encoding.UTF8.GetBytes(objLoginViewModel.Password);
            byte[] passwordCoversion = CryptoService.ComputeHMAC256(data: bytePassword, salt);
            string actualPassword = Convert.ToBase64String(passwordCoversion);

            if(objUser.UserPassword != actualPassword)
            {
                ModelState.AddModelError(key: "", errorMessage: "Email & Password is invalid.");
                return View(objLoginViewModel);

            }

            var roles = from objUserRole in objzeppelinDBEntities.UserRoles
                           join objRole in objzeppelinDBEntities.Roles on objUserRole.RoleId equals objRole.RoleId
                           select objRole.RoleName;


            string roleName = String.Join(separator:",", roles);

            FormsAuthentication.SetAuthCookie(objLoginViewModel.Email, createPersistentCookie: false);
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, objLoginViewModel.Email,DateTime.Now,DateTime.Now.AddMinutes(5),isPersistent: false, userData:roleName);

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);
            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, value:encryptedTicket);
            HttpContext.Response.Cookies.Add(authCookie);

            return RedirectToAction("Index", controllerName: "Home");
        }



        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserViewModel objUserViewModel)
        {
            if (ModelState.IsValid)
            {
                User objUser = new User();
                string VerificationCode = Guid.NewGuid().ToString();
                objUser.UserName = objUserViewModel.UserName;
                objUser.Email = objUserViewModel.UserEmail;
                objUser.FirstName = objUserViewModel.FirstName;
                objUser.LastName = objUserViewModel.LastName;
                var UserSalt = CryptoService.GenerateSalt();
                var hmac = CryptoService.ComputeHMAC256(Encoding.UTF8.GetBytes(objUserViewModel.UserPassword),
                    UserSalt);
                objUser.UserPassword = Convert.ToBase64String(hmac);
                objUser.UserSalt = Convert.ToBase64String(UserSalt);
                objUser.VerificationCode = VerificationCode;
                objUser.IsVerified = false;
                objzeppelinDBEntities.Users.Add(objUser);
                objzeppelinDBEntities.SaveChanges();

                var activateURL = "/Account/ActivateAccount/" + VerificationCode;
                var activateLink = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery,newValue: activateURL);

                EmailVerification.SendVerficationLink(objUserViewModel.UserEmail, activateLink);
                return View();
            }


            return View();
        }

        public JsonResult UserNameExists(string userName)
        {
            bool isUserNameExists = objzeppelinDBEntities.Users.Any(model => model.UserName == userName);
            return Json(!isUserNameExists, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EmailExist(string email)
        {

            bool isEmailExists= objzeppelinDBEntities.Users.Any(model => model.Email == email);
            return Json(!isEmailExists, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ActivateAccount(string id)
        {
            User objUser = objzeppelinDBEntities.Users.Single(model => model.VerificationCode == id);
            objUser.IsVerified = true;
            objzeppelinDBEntities.SaveChanges();


            UserRole objUserRole = new UserRole();
            objUserRole.RoleId = 1;
            objUserRole.UserId = objUser.UserId;
            objzeppelinDBEntities.UserRoles.Add(objUserRole);
            objzeppelinDBEntities.SaveChanges();
            ViewBag.Email = objUser.Email;
            return View();

        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, value: "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie);

            return RedirectToAction("Index", controllerName: "Home");



        }



    }
}