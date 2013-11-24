using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PSBlog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

          

            routes.MapRoute(
               name: "BlogShortcut",
               url: "b/{blogSlug}",
               defaults: new { controller = "Blog", action = "Details", blogSlug = "" }
           );

            routes.MapRoute(
          name: "PostShortcut",
          url: "b/{blogSlug}/{postSlug}",
          defaults: new { controller = "Post", action = "Details", blogSlug = "", postSlug = "" }
      );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}