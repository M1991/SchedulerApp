using Microsoft.AspNet.Identity.EntityFramework;
using SchedulerApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using static SchedulerApp.Models.ApplicationDbContext;

namespace SchedulerApp.Controllers
{
    [Authorize]
    public class RolesController : Controller
    {
        ApplicationDbContext adb = new ApplicationDbContext();
        // GET: Roles
        public ActionResult Index()
        {
            var TRoles = adb.Roles.ToList();
            //ViewBag.RoleAssg = Roles.GetUsersInRole(TRoles.ToString());
            return View(TRoles);           
        }

        /// <summary>
        /// Create  a New role
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            var Role = new IdentityRole();
            return View(Role);
        }

        /// <summary>
        /// Create a New Role
        /// </summary>
        /// <param name="Role"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(IdentityRole Role)
        {
            // IQueryable ir;
            //ir = adb.Roles.Where(m => m.Name.Equals(Role.ToString())).FirstOrDefault<IdentityRole>();
            //var iqe = adb.Roles.Where(m => m.Name.Equals(Role.ToString())).FirstOrDefault<IdentityRole>();
            //var iqe = adb.Roles.Where(m => m.Name.Equals(Role.ToString())).Select(m =>m.Name);
            var rol = Role.Name;
            //List<string> st = new List<string>();
            //  st = Roles.GetAllRoles().ToList();
            var st = adb.Roles.ToList();
            foreach(var item in adb.Roles)
            {
                if(item.Name == rol)
                {
                    ViewBag.Message = "Role Name " +rol +" already exist in Roles List";
                    return View();
                }                
                   
            }
            adb.Roles.Add(Role);
            adb.SaveChanges();
            return RedirectToAction("Index");

            //  if (st.Contains(rol))

            //Check with foreach loop if role exist before creation
            //    if (iqe == null)

        }

        public ActionResult Delete(IdentityRole RoleName)
        {
            if(RoleName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var entry = adb.Entry(RoleName);
            if (entry.State == EntityState.Detached)
                adb.Roles.Attach(RoleName);
            //adb.myTable.Remove(RoleName);
            
            adb.Roles.Remove(RoleName);
            adb.SaveChanges();
            ViewBag.Message = "Role deleted succesfully !";
            return RedirectToAction("Index", "Roles");
        }

    }
}