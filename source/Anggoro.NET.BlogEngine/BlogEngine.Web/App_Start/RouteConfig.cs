using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BlogEngine.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "HomePage",
                "",
                new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                "Error",
                "Oops",
                new { controller = "Home", action = "Error" }
            );

            routes.MapRoute(
                "BlogPost",
                "{postName}",
                new { controller = "Home", action = "ViewBlogPost", postName = "" }
            );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
