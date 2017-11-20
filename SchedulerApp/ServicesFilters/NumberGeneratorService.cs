using SchedulerApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using static SchedulerApp.Models.ApplicationDbContext;

namespace SchedulerApp.ServicesFilters
{
    public class NumberGeneratorService
    {
    }

    public class AutoUserIncrementer
    {
        private ApplicationDbContext dbc = new ApplicationDbContext();
        public AutoUserIncrementer()
        {
            var incc=0;
            var incCount= dbc.DbEvents.Where(x => x.eventId > 0).Count();
           
            if(incCount.Equals(0))
            {
                IncrementUserNumb(0);
            }
            else
            {
                incc = dbc.DbEvents.Where(x => x.eventId > 0).Count(); 
                //incc = dbc.DbEvents.OrderByDescending(m => m.eventId).Select(m => m.eventId).First();
            }
            if ((!double.IsNaN(incc)) || (!incc.Equals(null)))
            {
                IncrementUserNumb(incc);
            }
            else
            {
                IncrementUserNumb(0);
            }
        }
        public int _incrementer { get; set; }
        public int _originalNumber { get; set; }

        public void IncrementUserNumb(int d)
        {
            this._originalNumber = d;
            this._incrementer = d + 1;
            HttpContext.Current.Session["userIncrementer"] = this._incrementer;
        }
    }

    public class FileUploadService
    {
        private int autoIncr;
        private ApplicationDbContext dbc = new ApplicationDbContext();

        public FileUploadService()
        {
            var incc = 0;
            var incCount = dbc.DbFileUpload.Where(x => x.soCopyId > 0).Count();

            if (incCount.Equals(0))
            {
                IncrementUserNumb(0);
            }
            else
            {
                incc = dbc.DbFileUpload.OrderByDescending(m => m.soCopyId).Select(m => m.soCopyId).First();
            }
            if ((!double.IsNaN(incc)) || (!incc.Equals(null)))
            {
                IncrementUserNumb(incc);
            }
            else
            {
                IncrementUserNumb(0);
            }
        }
        public int _incrementer { get; set; }
        public int _originalNumber { get; set; }

        public void IncrementUserNumb(int d)
        {
            this._originalNumber = d;
            this._incrementer = d + 1;
            HttpContext.Current.Session["FileUploadId"] = this._incrementer;
        }

        public void SaveFileDetails(HttpPostedFileBase fileBase)
        {
            FileUploadEvents eM = new FileUploadEvents();
            eM.soCopyType = fileBase.ContentType;
            eM.soCopyData = ConvertToBytes(fileBase);
           // using (EventsModels evm = new EventsModels())
          //  {
                /// Save the details for DB chages
             //   evm.
          //  }
        }

        public byte[] ConvertToBytes(HttpPostedFileBase fileBase)
        {
            byte[] imgBytes = null;
            BinaryReader reader = new BinaryReader(fileBase.InputStream);
            imgBytes = reader.ReadBytes((int)fileBase.ContentLength);
            return imgBytes;
        }

    }

    public class LastLoginStorage
    {
        //private ApplicationDbContext adbcontext = new ApplicationDbContext();

        /// <summary>
        /// Check for Mining lastlogins to track recent users, get their update to other users
        /// 
        /// 
        /// </summary>
        public LastLoginStorage()
        {

        }
    }

    public class UserIdGenerator
    {
        private ApplicationDbContext adbcontext = new ApplicationDbContext();

        private int _genrator = 101;
        private int _ufgenrator = 1;
        public UserIdGenerator()
        {
            
            var sttotal = adbcontext.Users.Where(m => m.UserId > 0).Count();
            if (sttotal.Equals(0))
            {
                _genrator = 101;
                _ufgenrator = 1;
            }
            else
            {
                _genrator = _genrator + sttotal;
                _ufgenrator = _ufgenrator + sttotal;
                // sttotal = adbcontext.Users.OrderByDescending(m => m.UserId).Select(m => m.UserId).First();
            }

            // if ((!double.IsNaN(sttotal)) || (!sttotal.Equals(null)))
            //  {
            //        _genrator = _genrator;
            //    }
            //    else
            //    {
           // _genrator = _genrator + 1;
        //    }  
        }

        public int UserIdSender()
        {
            return _genrator;
        }

        public int UserIdInfoSender()
        {
            
            return _ufgenrator;
        }
    }
}