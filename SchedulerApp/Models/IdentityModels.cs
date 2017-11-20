using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SchedulerApp.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public virtual string Accesslvl { get; set; }
        public virtual string Fname { get; set; }
        public virtual string Lname { get; set; }
        public virtual DateTime LastLogin { get; set; }
        public virtual int UserId { get; set; }

        //public virtual ICollection<TUser> Users { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            //: base("DefaultConnection", throwIfV1Schema: false) 
            : base("EventsModel", throwIfV1Schema: false)
        {
        }
         
        public class ApplicationRole : IdentityRole
        {
            public ApplicationRole() : base() { }
            public ApplicationRole(string name, string description) : base(name)
            {
                this.Description = description;
            }
            public virtual string Description { get; set; }           
        }

        [Table("Events")]
        public class Events
        {
            [Key]
            //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public Guid uId { get; set; }
             public Events()
             {
                uId = Guid.NewGuid();
             }
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

          //  [Required]
          //  [Display(Name = "Event Start Time")]
           // public System.TimeSpan startTime { get; set; }

          //  [Display(Name = "Event End Time")]
         //   public System.TimeSpan endTime { get; set; }
            public string eDesc { get; set; }
            public string trigPerson { get; set; }           
            public string PoNo { get; set; }
            public string PoStatus { get; set; }

            [Required]
            [System.Web.Mvc.Remote("IsSoExist", "Events", ErrorMessage = "S.O. number already exist", HttpMethod = "POST")]
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
            public System.DateTime dtCreated { get; set; }
            //public DateTime? DeliveryDate { get; set; }
            public virtual SoNosEvent SoNosEvent { get; set;}
        }

        [Table("UsersInfo")]
        public class UsersInfo
        {
            [Key]
            public int Id { get; set; }           
            public string UserEmail { get; set; }
            public int UserVerify { get; set; }
            public string UserDept { get; set; }
            public string UserFname { get; set; }
            public DateTime LastLogin { get; set; }
            public DateTime LastLogout { get; set; }
        }

        [Table("EventsPartNos")]
        public class SoNosEvent
        {
            [Key]
            public int id { get; set; }
            public Guid uUid { get; set; }
            public string SoNo { get; set; }
            public bool AddNewPartNo { get; set; }           
            public string MultiplePartNo { get; set; }
            public int MultiplePartQtyNo { get; set; }
            public string HCategory { get; set; }
            public decimal HDia { get; set; }           
            public decimal HLength { get; set; }            
            public string Accessr { get; set; }           
            public decimal HLLength { get; set; }
            public string LeadProtect { get; set; }
            public string Potting { get; set; }
            public string Hdesc { get; set; }
            /* public virtual ICollection<String> MultiplePartNo { get; set; }
             public SoNosEvent()
             {
                 MultiplePartNo = new List<String>();
             } */           
        }

        [Table("FileUploadEvents")]
        public class FileUploadEvents
        {
            [Key]
            public int soCopyId { get; set; }
            public Guid soEventId { get; set; }
            public string soCopyType { get; set; }
            public string soNoRef { get; set; }
            [DataType(DataType.Upload)]
            [Display(Name = "Select File")]
            public byte[] soCopyData { get; set; }
        }

        protected override void OnModelCreating(DbModelBuilder modelbuilder)
        {
            if(modelbuilder == null)
            {
                throw new ArgumentNullException("modelbuilder");
            }

           base.OnModelCreating(modelbuilder); // this needs to go before the other rules!
           
            modelbuilder.Entity<IdentityUser>().ToTable("Users");
            EntityTypeConfiguration<ApplicationUser> table = modelbuilder.Entity<ApplicationUser>().ToTable("Users");
            table.Property((ApplicationUser u) => u.UserName).IsRequired();

            modelbuilder.Entity<ApplicationUser>().HasMany<IdentityUserRole>((ApplicationUser u) => u.Roles);
            modelbuilder.Entity<IdentityUserRole>().HasKey((IdentityUserRole r) =>
                new { UserId = r.UserId, RoleId = r.RoleId }).ToTable("UserRoles");

            // Leave this alone:
            EntityTypeConfiguration<IdentityUserLogin> entityTypeConfiguration =
                modelbuilder.Entity<IdentityUserLogin>().HasKey((IdentityUserLogin l) =>
                    new {
                        UserId = l.UserId,
                        LoginProvider = l.LoginProvider,
                        ProviderKey
                        = l.ProviderKey
                    }).ToTable("UserLogins");
         modelbuilder.Entity<IdentityUserClaim>().ToTable("UserClaims"); 

            // Add this, so that IdentityRole can share a table with ApplicationRole:
            modelbuilder.Entity<IdentityRole>().ToTable("Roles");

            // Change these from IdentityRole to ApplicationRole:
            EntityTypeConfiguration<ApplicationRole> entityTypeConfiguration1 =
                modelbuilder.Entity<ApplicationRole>().ToTable("Roles");
         
        }
     
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual DbSet<Events> DbEvents { get; set; }
        public virtual DbSet<FileUploadEvents> DbFileUpload{ get; set; }
        public virtual DbSet<SoNosEvent> SoNosEvents { get; set; }
        public virtual DbSet<UsersInfo> DbUsersInfo { get; set; }
        public virtual DbSet<IdentityUser> DbUsers { get; set; }

        //  public System.Data.Entity.DbSet<SchedulerApp.Models.Event> Events { get; set; }

    }
}