using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
//using NextFlex_Configurator.Models;
//using System.Activities;
using System.Data;
using SchedulerApp.Models;
using SchedulerApp.ServicesFilters;
using Microsoft.AspNet.Identity.EntityFramework;
//using NextFlex_Configurator.Services;

namespace NextFlex_Configurator.Filters
{
    /*
     * interface SqlExceptionFilter : IExceptionFilter
    {

    }

    */
    

    public class AccessLvl
    { 
        public enum AccessDesc
        {
            Sales = 0,
            Accounts =	1,
            Dispatch = 2,
            OpenVariable = 3,
            Production = 4,
            Purchase =	5,
            QC = 6,
            Admin = 7
        };

        public static int AccessCount
        {
            get
            {
                return System.Enum.GetNames(typeof(AccessDesc)).Length;
            }
        }
    }
  
   public class RolesContents
    {
        public int MyProperty { get; set; }
    }

   public class AccessDeniedAuthorizationAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (filterContext.Result is HttpUnauthorizedResult)
            {
                //  Access Deined, You will be migrated to another page
                filterContext.Result = new RedirectResult("~/Shared/AuthorizeFailed");
            }
        }
    }

    public class RoleAllowAttribute : AuthorizeAttribute
    {
        ApplicationDbContext context = new ApplicationDbContext(); // my entity  
        private readonly string[] allowedroles;
        private readonly string[] roleName;
        private string UserRoleName;
        public string tempTest { get; set; }    

        string emTest = System.Web.HttpContext.Current.User.Identity.GetEmailAdress();
      //  object currAccess;
        
        public RoleAllowAttribute(params string[] roles)
        {
            this.roleName = roles;
            this.allowedroles = roles;           
            // emTest = System.Web.HttpContext.Current.User.Identity.GetEmailAdress();
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            int k = 0;
            bool authorize = false;
            string[] dsays = Enum.GetNames(typeof(AccessLvl.AccessDesc));
            
            if (HttpContext.Current.Session["UserDept"] != null)
            {
                //Getting the type of User AccessLevel
                //tempTest = System.Web.HttpContext.Current.User.Identity.GetEmailAdress();
                tempTest = HttpContext.Current.Session["UserDept"].ToString();
            }
            else
            {
              var currAccess = context.DbUsers.Where(m => m.Email.Equals(emTest)).FirstOrDefault();
                tempTest = currAccess.Email;
            }
            foreach (var role in allowedroles)
                {
               
                    for(int j=0; j< AccessLvl.AccessCount; j++)
                    {
                        if(roleName[k] == dsays[j])
                        {
                            UserRoleName = dsays[j];
                        }
                    }                 
                if (tempTest.Equals(UserRoleName))
                {                
                   authorize = true;
                }
                k++;             
            }
            return authorize;
        }

        public bool AuthDelRole(string roleName)
        {
            int acs=0, k = 0;
            bool authorize = false;
            string[] dsays = Enum.GetNames(typeof(AccessLvl.AccessDesc));
            if (HttpContext.Current.Session["UserDept"] != null)
            {
                var act = Int32.TryParse(HttpContext.Current.Session["UserDept"].ToString(), out acs);
                var tempTest = HttpContext.Current.Session["UserDept"].ToString();
            }
            foreach (var role in allowedroles)
            {
                for (int j = 0; j < AccessLvl.AccessCount; j++)
                {
                    if (roleName == dsays[j])
                    {
                        UserRoleName = dsays[j];
                    }
                }

                if (acs.Equals(UserRoleName))
                {
                    authorize = true;
                }
                k++;
            }
            return authorize;
        }

        public bool AuthRole(params string[] roleName)
        {
            int k = 0;
            bool authorize = false;
           // var tempTest="";
            string[] dsays = Enum.GetNames(typeof(AccessLvl.AccessDesc));
            if (HttpContext.Current.Session["UserDept"] != null)
            {
                //var act = Int32.TryParse(HttpContext.Current.Session["UserDept"].ToString(), out acs);
                tempTest = HttpContext.Current.Session["UserDept"].ToString();
            }
            else
            {
                var currAccess = context.DbUsers.Where(m => m.Email.Equals(emTest)).FirstOrDefault();
                tempTest = currAccess.Email;
            }
            foreach (var role in allowedroles)
            {
                for (int j = 0; j < AccessLvl.AccessCount; j++)
                {
                    if (roleName[k] == dsays[j])
                    {
                        UserRoleName = dsays[j];
                    }
                }

                if (tempTest.Equals(UserRoleName))
                {
                    authorize = true;
                }
                k++;
            }
            return authorize;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult();
        }
        ///  public bool IsSysAdmin { get { return this.Roles.IsSysAdmin; } }

    }
    
    
}
