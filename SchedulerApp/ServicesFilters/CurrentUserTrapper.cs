using Microsoft.AspNet.Identity;
using SchedulerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace SchedulerApp.ServicesFilters
{
    public class CurrentUserTrapper
    {
        public int _userId;
        public string _userName;
        public string _userEmail;
        public string somename2;
        public string somename4;

        public CurrentUserTrapper()
        { }

        public void SomeFunction(LoginViewModel model)
        {
           // this._userId = model.UserID;
           // this._userName = model.UserFname;
        }

    }

    public static class IdentityExtensions
    {
        public static string GetEmailAdress(this IIdentity identity)
        {
            var userId = identity.GetUserId();
            using (var context = new ApplicationDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Id == userId);
                return user.Email;
            }
        }
    }
}