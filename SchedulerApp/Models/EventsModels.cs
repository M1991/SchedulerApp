namespace SchedulerApp.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;
    using System.Linq;

    public class EventsModels : DbContext
    {
        // Your context has been configured to use a 'EventsModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'SchedulerApp.Models.EventsModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'EventsModel' 
        // connection string in the application configuration file.
        public EventsModels()
            : base("name=EventsModel")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

       // public virtual DbSet<Events> DbEvents { get; set; }
       // public virtual DbSet<FileUpload> DbEvents { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}

    ///
    /*public class Events
    {
        [Key]
        
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
        public System.DateTime dtCreated { get; set; }
        public bool allDay { get; set; }
        public string color { get; set; }

        [Required]
        [Display(Name = "Event Start Time")]
        public System.TimeSpan startTime { get; set; }

        [Display(Name = "Event End Time")]
        public System.TimeSpan endTime { get; set; }
        public string eDesc { get; set; }
        public string trigPerson { get; set; }
        public string JoNo { get; set; }
        public string SONo { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Select File")]
        public string soCopy { get; set; }
        public string nowAtStatn { get; set; }
        public string partNo { get; set; }
        public string comments { get; set; }
    }*/

}