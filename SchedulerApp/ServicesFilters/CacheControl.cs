using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace NextFlex_Configurator.Filters
{
   
    ///[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public sealed class CacheControlAttribute : ActionFilterAttribute
    {
        private readonly HttpCacheability cacheability;
        public HttpCacheability Cacheability { get { return this.cacheability; } }
        public CacheControlAttribute(HttpCacheability cacheability)
        {
            this.cacheability = cacheability;
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            HttpCachePolicyBase cache = filterContext.HttpContext.Response.Cache;
            cache.SetCacheability(this.cacheability);
        }
    }

    public class NoCacheAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
            filterContext.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            filterContext.HttpContext.Response.Cache.SetNoStore();

            base.OnResultExecuting(filterContext);
        }
    }

    public class InternationalizationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string language = (string)filterContext.RouteData.Values["language"] ?? "nl";
            string culture = (string)filterContext.RouteData.Values["culture"] ?? "NL";

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(string.Format("{0}-{1}", language, culture));
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(string.Format("{0}-{1}", language, culture));
        }
    }
}