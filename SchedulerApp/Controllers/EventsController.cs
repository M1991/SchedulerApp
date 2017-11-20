using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SchedulerApp.Models;
using SchedulerApp.ServicesFilters;
using System.IO;
using static SchedulerApp.Models.ApplicationDbContext;
using NextFlex_Configurator.Filters;

namespace SchedulerApp.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
      //  private EventsModels db = new EventsModels();
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Events
        public ActionResult Index()
        {
            return View(db.DbEvents.ToList());
        }

        // GET: Events/CalendarView
        public ActionResult CalendarView()
        {
            /// Check for last added event, and display as last added event, only for sales person
            ViewBag.EventStatus = String.Empty;
            AutoUserIncrementer acm = new AutoUserIncrementer();
            return View();
        }

        //GET:Display the events in calendar
        public JsonResult GetCalDispEvents()
        {
            // var sttrd = db.DbEvents.Where(m => m.eventId > 0).FirstOrDefault();
            //  DataTable projSchDt = db.DbEvents.getProjectsSchedule(ccUser.CompanyId, start, end, ccUser.Id, true);
            // db.DbEvents.ToList()
            //var events = new List<Models.Events>();

            // using (Models.Events sddb = new Models.Events())
            using (var ctx = new ApplicationDbContext())            
                {
                    var events = from cevent in db.DbEvents select cevent;
                    var rows = events.ToList().Select(cevent => new {
                        id = cevent.eventId,
                        title = cevent.title,
                        // Getting the colors as users set
                        color = cevent.color,
                        start = cevent.startt.AddSeconds(1).ToString("yyyy-MM-ddTHH:mm:ssZ"),
                        end = cevent.endt.ToString("yyyy-MM-ddTHH:mm:ssZ")
                    }).ToArray();
                    return Json(rows, JsonRequestBehavior.AllowGet);
                }
            

            //   var rows = events.ToArray();
            //   return Json(rows, JsonRequestBehavior.AllowGet);
        }

        
        public JsonResult GetCalendar(double start, double end)
        {
            var events = new List<Models.Events>();
            var dtstart = Convert.ToDateTime(start);
            var dtend = Convert.ToDateTime(end);
            var onCalls = from oc in db.DbEvents
                          where oc.startt >= dtstart
                          select oc;

            DateTime currStart;
            DateTime currEnd;
            foreach (var oc in onCalls)
            {
                currStart = Convert.ToDateTime(oc.startt);
                currEnd = Convert.ToDateTime(oc.endt);
                events.Add(new Models.Events
                {
                    eventId = oc.eventId,
                    title = oc.title,
                    startt = currStart,
                    endt = currEnd,
                    allDay = false
                   // url = "/Schedule/Details/" + oc.eventId.ToString() + "/"
                });
            }
            var rows = events.ToArray();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }

        /*
        public JsonResult GetEvents()
        {
            var elc = FCEvents.LoadAllAppointmentsInDateRange();
            var EventList =
                from e in elc
                select new
                {
                    id = e.ID,
                    title = e.EventTitle,
                    start = e.StartDateString,
                    end = e.EndDateString
                };

            var rows = elc.ToArray();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
*/

        //GET: /Events/FullEventDetails
        public ActionResult FullEventDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationDbContext.Events events = db.DbEvents.Find(id);
            if (events == null)
            {
                return HttpNotFound();
            }
            return View();
        }


        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationDbContext.Events events = db.DbEvents.Find(id);
            if (events == null)
            {
                return HttpNotFound();
            }
            return View(events);
        }

        // GET: Events/Create
        [Authorize]
        [RoleAllow("Sales","Admin")]
        public ActionResult CreateTemp()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        //[RoleAllow("Sales","Admin")]
        public ActionResult Create([Bind(Include = "uId,eventId,etDept,title,startt,endt,dtCreated,proCustomer,allDay,color,startTime,endTime,eDesc,trigPerson,SONo,SoStatus,soCopy,nowAtStatn,partNo,comments,EventsStatus,backgColor,borderColor,textColor,JoNo,PoNo,TotpartNo")] ApplicationDbContext.Events events, FormCollection fc, ManageBothViewModel mvb)
        {
            if (ModelState.IsValid)
            {
                ///Date and time calculation here before adding the event with reqd time and expected delivery date
                ///Start date -> last end datetime + 1 min
                ///End date -> according to order details
                if (mvb.FileUploadModel.soCopyData.ContentLength > 0)
                {
                    ///  var fileName = Path.GetFileName(files.FileName);
                    ///  var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                    ///  files.SaveAs(path);
                    ///  
                    ///    Save to FileUploadEvents
                    ///    String FileExt = Path.GetExtension(files.FileName).ToUpper();
                    FileUploadService fus = new FileUploadService();
                    var ele = mvb.SoNoEventds.MultiplePartNo.Count();
                    var sbc = new ApplicationDbContext.SoNosEvent();
                    if (mvb.Events.SONo!= null)
                    {
                        sbc.id = mvb.Events.eventId;
                        
                        sbc.SoNo = mvb.Events.SONo;
                      //  foreach (var i in ele)
                      for(int i=0; i < ele; i++)
                        {
                            if(mvb.SoNoEventds.MultiplePartNo != null)
                            {
                                sbc.AddNewPartNo = true;
                                sbc.uUid = events.uId;
                                sbc.MultiplePartNo = mvb.SoNoEventds.MultiplePartNo[i];
                                sbc.MultiplePartQtyNo = mvb.SoNoEventds.MultiplePartQtyNo[i];
                                sbc.SoNo = mvb.Events.SONo;
                                db.SoNosEvents.Add(sbc);
                                db.SaveChanges();
                            }
                        } 
                    } 
                    String FileExt = Path.GetExtension(mvb.FileUploadModel.soCopyData.FileName).ToUpper();

                    if (FileExt == ".PDF")
                    {
                        Stream str = mvb.FileUploadModel.soCopyData.InputStream;
                        BinaryReader Br = new BinaryReader(str);
                        Byte[] FileDet = Br.ReadBytes((Int32)str.Length);

                        FileUploadEvents Fd = new Models.ApplicationDbContext.FileUploadEvents();
                        Fd.soCopyType = mvb.FileUploadModel.soCopyData.ContentType;
                        Fd.soNoRef = events.SONo;
                        Fd.soCopyData = FileDet;
                        Fd.soCopyId = events.eventId;
                        Fd.soEventId = events.uId;
                        events.TotpartNo = ele;
                        events.SoNosEvent = sbc;
                        db.DbFileUpload.Add(Fd);
                     // db.SoNosEvents.Add(sbc);
                        db.DbEvents.Add(events);
                        db.SaveChanges();
                        ViewBag.EventStatus = events.eventId;
                        return RedirectToAction("CalendarView");
                    }
                    //var tempvar = int.TryParse(fus._incrementer.ToString(), out int somTemp);
                   // events.soCopy = Session[""].ToString();
                    return RedirectToAction("CalendarView");
                }
                else
                {
                    ModelState.AddModelError(String.Empty, "Invalid format for Uploaded file. Use only PDF formats");
                    return View();
                   // return 
                }
               // FileUploadService fuservice = new FileUploadService();
               // foreach(var item in FileUploadModel.files)
               // {
               //     fuservice.SaveFileDetails(item);
               // }               
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                ModelState.AddModelError(String.Empty, "Username already in use");
            }
            return View("CalendarView");
        }


        /// <summary>
        /// Not in use, function not triggering, both submit button triggers for same actionb result
        /// </summary>
        /// <param name="fcoll"></param>
        /// <returns></returns>
        [HttpPost]       
        [ActionName("IsSoExist")]        
        public ActionResult IsSoExist([Bind(Prefix = "Events.SONo")]string SONo)
        {
            return Json(IsSoAvailable(SONo));
        }
        public bool IsSoAvailable(string SONo)
        {
            // Assume these details coming from database  
            var partAvl = (from u in db.DbEvents
                           where u.SONo == SONo
                           select new { SONo }).FirstOrDefault();
            bool status;
            if (partAvl != null)
            {
                //Already registered  
                status = false;
            }
            else
            {
                //Available to use  
                status = true;
            }
            return status;
        }


        // GET: Events/Edit/5
        //[RoleAllow("Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationDbContext.Events events = db.DbEvents.Find(id);
            if (events == null)
            {
                return HttpNotFound();
            }
            return View(events);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[RoleAllow("Admin")]
        public ActionResult Edit([Bind(Include = "uId,eventId,etDept,title,startt,endt,dtCreated,proCustomer,allDay,color,startTime,endTime,eDesc,trigPerson,SONo,SoStatus,soCopy,nowAtStatn,partNo,comments,EventsStatus,backgColor,borderColor,textColor,JoNo,PoNo,TotpartNo")] ApplicationDbContext.Events events)
        {
            if (ModelState.IsValid)
            {
                db.Entry(events).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(events);
        }

        // GET: Events/Delete/5
        //[RoleAllow("Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
             ApplicationDbContext.Events events = db.DbEvents.Find(id);
            // ApplicationDbContext.FileUploadEvents events = db.DbFileUpload.Find(id);

            if (events == null)
            {
                return HttpNotFound();
            }
            return View(events);
        }

        // POST: Events/Delete/5
        //[RoleAllow("Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ApplicationDbContext.Events events = db.DbEvents.Find(id);
            db.DbEvents.Remove(events);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        // Add a function
    }
}