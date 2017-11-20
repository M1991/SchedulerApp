using SchedulerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchedulerApp.ServicesFilters
{
    public class AuthFilterAttribute : AuthorizeAttribute
    {
        public AuthFilterAttribute()
        {
            View = "AuthorizeFailed";
        }

        public string View { get; set; }

        /// <summary>
        /// Check for Authorization
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            IsUserAuthorized(filterContext);
        }

        /// <summary>
        /// Method to check if the user is Authorized or not
        /// if yes continue to perform the action else redirect to error page
        /// </summary>
        /// <param name="filterContext"></param>
        private void IsUserAuthorized(AuthorizationContext filterContext)
        {
            // If the Result returns null then the user is Authorized 
            if (filterContext.Result == null)
                return;

            //If the user is Un-Authorized then Navigate to Auth Failed View 
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {

                // var result = new ViewResult { ViewName = View };
                var vr = new ViewResult();
                vr.ViewName = View;

                ViewDataDictionary dict = new ViewDataDictionary();
                dict.Add("Message", "Sorry you are not Authorized to Perform this Action");

                vr.ViewData = dict;

                var result = vr;

                filterContext.Result = result;
            }
        }
    }

    
     public class FCEvents
    {
        public int ID;
        public string EventTitle;
        public string StartDateString;
        public string EndDateString;
        public string Color;

        public static List<FCEvents> LoadAllAppointmentsInDateRange()
        {

            using (ApplicationDbContext ev = new ApplicationDbContext())
            {
                var rslt =
                from e in ev.DbEvents
                select new Events
                {
                    eventId = e.eventId,
                    title = e.title,                   
                    startt = e.startt,
                    endt = e.endt
                   // Location = e.Location
                };

                List<FCEvents> result = new List<FCEvents>();

                foreach (var item in rslt)
                {
                    FCEvents rec = new FCEvents();
                    rec.ID = item.eventId;
                    rec.EventTitle = item.title;
                    rec.StartDateString = item.startt.ToString("s");
                    rec.EndDateString = item.endt.ToString("s");
                    result.Add(rec);
                }
                return result;
            }

        }
    }
}