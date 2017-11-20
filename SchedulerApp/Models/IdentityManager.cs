using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static SchedulerApp.Models.ApplicationDbContext;

namespace SchedulerApp.Models
{
    public class IdentityManager
    {
        // Swap ApplicationRole for IdentityRole:
        RoleManager<ApplicationRole> _roleManager = new RoleManager<ApplicationRole>(
            new RoleStore<ApplicationRole>(new ApplicationDbContext()));

        UserManager<ApplicationUser> _userManager = new UserManager<ApplicationUser>(
            new UserStore<ApplicationUser>(new ApplicationDbContext()));

        ApplicationDbContext _db = new ApplicationDbContext();


        public bool RoleExists(string name)
        {
            return _roleManager.RoleExists(name);
        }


        public bool CreateRole(string name, string description = "")
        {
            // Swap ApplicationRole for IdentityRole:
            var idResult = _roleManager.Create(new ApplicationRole(name, description));
            return idResult.Succeeded;
        }


        public bool CreateUser(ApplicationUser user, string password)
        {
            var idResult = _userManager.Create(user, password);
            return idResult.Succeeded;
        }


        public bool AddUserToRole(string userId, string roleName)
        {
            var idResult = _userManager.AddToRole(userId, roleName);
            return idResult.Succeeded;
        }


        public void ClearUserRoles(string userId)
        {
            var user = _userManager.FindById(userId);
            var currentRoles = new List<IdentityUserRole>();

            currentRoles.AddRange(user.Roles);
            foreach (var role in currentRoles)
            {
                _userManager.RemoveFromRole(userId, role.RoleId);
            }
        }

        bool AddUserAndRoles()
        {
            bool success = false;
            var idManager = new IdentityManager();
            // Add the Description as an argument:
            success = idManager.CreateRole("Admin", "Global Access");
            if (!success == true) return success;
            // Add the Description as an argument:
            success = idManager.CreateRole("Sales", "Sales Department");
            if (!success == true) return success;
            success = idManager.CreateRole("Production", "Production Department");
            if (!success == true) return success;
            success = idManager.CreateRole("Accounts", "Acccounts Department");
            if (!success == true) return success;
            // Add the Description as an argument:
            success = idManager.CreateRole("User", "Restricted to business domain activity");
            if (!success) return success;
            // While you're at it, change this to your own log-in:
            /*
            var newUser = new ApplicationUser()
            {
                UserName = "jatten",
                FirstName = "John",
                LastName = "Atten",
                Email = "jatten@typecastexception.com"
            };
            // Be careful here - you  will need to use a password which will 
            // be valid under the password rules for the application, 
            // or the process will abort:
            success = idManager.CreateUser(newUser, "Password1");
            if (!success) return success;
            success = idManager.AddUserToRole(newUser.Id, "Admin");
            if (!success) return success;
            success = idManager.AddUserToRole(newUser.Id, "CanEdit");
            if (!success) return success;
            success = idManager.AddUserToRole(newUser.Id, "User");
            if (!success) return success;
            */
            return success;
        }
    }

}