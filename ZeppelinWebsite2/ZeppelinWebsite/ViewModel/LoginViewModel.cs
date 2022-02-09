using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ZeppelinWebsite.ViewModel
{
    public class LoginViewModel
    {
        [Display(Name ="Email : ")]
        [Required(ErrorMessage ="Email is required.")]
        [EmailAddress(ErrorMessage ="Enter Valid Email Address.")]
        public string Email { get; set; }

        [Display(Name = "Password : ")]
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

    }
}