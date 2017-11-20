using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Web;
using static SchedulerApp.Models.ApplicationDbContext;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace SchedulerApp.Models
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
    }

    [Table("FileUploadEvents")]
    public class FileUploadModel
    {
        [Key]
        public int soCopyId { get; set; }
        public Guid soEventId { get; set; }
        public string soNoRef { get; set; }
        public string soCopyType { get; set; }
        [Required]
     //   [DataType(DataType.Upload)]
        [Display(Name = "Select File")]
        public HttpPostedFileBase soCopyData { get; set; }
    }

    public class Events
    {
        
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Events()
        {
            uId = Guid.NewGuid();
        }
        [Key]
        public Guid uId { get; set; }
        public int eventId { get; set; }

        [Display(Name = "Department")]
        public string etDept { get; set; }

        [Display(Name = "Event Title")]
        public string title { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Event Start Date")]
        public System.DateTime startt { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Event End Date")]
        public System.DateTime endt { get; set; }

        public string proCustomer { get; set; }
        public bool allDay { get; set; }
        public string color { get; set; }
               
        public string eDesc { get; set; }
        public string trigPerson { get; set; }
       
        public string PoNo { get; set; }
        public string PoStatus { get; set; }

        [Required]
        [Display(Name ="S.O. Number")]
        [System.Web.Mvc.Remote("IsSoExist", "Events", ErrorMessage = "S.O. number already exist", HttpMethod ="POST")]
        public string SONo { get; set; }
       
        public string SoStatus { get; set; }
        public string JoNo { get; set; }
        public string nowAtStatn { get; set; }

        public int TotpartNo { get; set; }
        public string comments { get; set; }
        public string EventsStatus { get; set; }
        public string backgColor { get; set; }
        public string borderColor { get; set; }
        public string textColor { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date Created")]
        //public DateTime? DeliveryDate { get; set; }
        public System.DateTime dtCreated { get; set; }

        public SoNosEvent SoNosEventD { get; set; }
    }

   
    public class ManageBothViewModel
    {
        //  public virtual IEnumerable<ModifyPartNoModel> ModifyPartModels { get; set; }
        public Events Events { get; set; }
        //public Events Events{ get; set; }
        public FileUploadModel FileUploadModel { get; set; }
        //public virtual FileUploadEvents FileUploadEvents { get; set; }
        public SoNosEvent SoNoEventds { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class SoNosEvent
    {
        [Key]
        public int id { get; set; }
        public Guid uUid { get; set; }
        [Required]
        public string SoNo { get; set; }
        public bool AddNewPartNo{ get; set; }
        [Display(Name = "Part No.")]
        public virtual IList<String> MultiplePartNo { get; set; }
        [Required]
        [Display(Name = "Quantity")]
        public virtual IList<Int16> MultiplePartQtyNo { get; set; }
        [Required]
        [Display(Name = "Category")]
        public virtual IList<String> HCategory { get; set; }
        [Required]
        [Display(Name = "Diameter")]
        public virtual IList<Decimal> HDia { get; set; }
        [Required]
        [Display(Name = "Length")]
        public virtual IList<Decimal> HLength { get; set; }
        [Required]
        [Display(Name = "Accessories")]
        public virtual IList<String> Accessr { get; set; }
        [Required]
        [Display(Name = "Lead Length")]
        public virtual IList<Decimal> HLLength { get; set; }
        [Required]
        [Display(Name = "Potting")]
        public virtual IList<String> Potting { get; set; }
        [Required]
        [Display(Name = "Lead Protection")]
        public virtual IList<String> LeadProtect { get; set; }
        [Display(Name = "Description")]
        public virtual IList<String> Hdesc { get; set; }
        public SoNosEvent()
         {
            HCategory = new List<String>();
             MultiplePartNo = new List<String>();
             MultiplePartQtyNo = new List<Int16>();
            Hdesc = new List<String>();
            HDia = new List<Decimal>();
            HLength = new List<Decimal>();
            Accessr = new List<String>();
            HLLength = new List<Decimal>();
            Potting = new List<String>();
            LeadProtect = new List<String>();
        }     
        // public IList<String> MultiplePartNo { get; set; }
        // public IList<Int16> MultiplePartQtyNo { get; set; }*/
        //public string MultiplePartNo { get; set; }
        //public int MultiplePartQtyNo { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}