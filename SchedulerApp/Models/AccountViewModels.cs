using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SchedulerApp.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        [RegularExpression(@"^[a-z,A-Z]{1,12}\@[a-z,A-Z]{1,15}\.in$", ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        // [DataType(DataType.Date), DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy HH:mm}", ApplyFormatInEditMode = true)]
       // [DataType(DataType.DateTime)]
      //  public DateTime LastLogin { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
       // [Remote("IsPartAvailable", "ConfigNext", HttpMethod = "POST", ErrorMessage = "Part Number already exists in database.")]
        [RegularExpression(@"^[a-z,A-Z]{1,12}\@[a-z,A-Z]{1,15}\.in$", ErrorMessage = "Invalid Email Address, yourName@yourDepartment.in")]        
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Department")]        
        public string Accesslvl { get; set; }
        
        [Required]
        [Display(Name = "First Name")]
        public string UserFname { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string UserLname { get; set; }

        public int UserID { get; set; }
        //[DataType(DataType.Date), DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        //[DataType(DataType.DateTime)]
       // public DateTime LastLogin { get; set; }
    }

    public class TUser
    {
        [Required]
        [Display(Name = "Department")]
        public string Deprt { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string UserFname { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string UserLname { get; set; }

        public virtual ApplicationUser User { get; set; }
    }


    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
