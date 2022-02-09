using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Xunit;
using Xunit.Sdk;

namespace ZeppelinWebsite.ViewModel
{
    public class UserViewModel
    {
        [Display(Name ="User Name")]
        [Required(ErrorMessage ="User name is required.")]
        //[Remote("UserNameExist", "Account", ErrorMessage="User Name is already exists.")]
        public string UserName { get; set; }

        [Display(Name = "User Email")]
        [Required(ErrorMessage = "User name is required.")]
        [Remote("EmailExist", "Account", ErrorMessage = "User Name is already exists.")]
        [EmailAddress(ErrorMessage = "Enter valid email address.")]
        public string UserEmail { get; set; }


        [Display(Name = "User First Name")]
        [Required(ErrorMessage = "First name is required.")]
      
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }


        [Display(Name ="Password")]
        [Required(ErrorMessage = "Password is required.")]
        public string UserPassword { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Password is required.")]
        [System.Web.Mvc.Compare("UserPassword",ErrorMessage ="Password & Confirm Password should be same.")]
        public string ConfirmPassword { get; set; }

    }
}