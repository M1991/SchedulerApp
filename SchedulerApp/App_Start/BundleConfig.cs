using System.Web;
using System.Web.Optimization;

namespace SchedulerApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js", "~/Scripts/fullcalendar.js", "~/Scripts/jquery.timepicker.js", "~/Scripts/jquery-ui-1.12.1",
                        "~/Scripts/wickedpicker.js", "~/Scripts/ACustomScript.js"));

          //  bundles.Add(new ScriptBundle("~/bundles/angular").Include("~/Scripts/angular.js", "~/Scripts/AAngularCustom.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
                     "~/Scripts/moment.js", "~/Scripts/angular.js", "~/Scripts/AAngularCustom.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",                      
                      "~/Scripts/respond.js",
                       "~/Scripts/bootstrap-datetimepicker.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css", "~/Content/themes/base/jquery-ui.css", "~/Content/fullcalendar.css", "~/Content/bootstrap-datetimepicker.css", "~/Content/jquery.timepicker.css",
                      "~/Content/site.css", 
                      "~/Content/themes/base/datepicker.css"));
        }
    }
}
