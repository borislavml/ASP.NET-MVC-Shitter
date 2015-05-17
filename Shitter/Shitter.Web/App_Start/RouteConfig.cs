using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Shitter.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Users",
                url: "users/{username}",
                defaults: new { controller = "Users", action = "Index"}
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{page}",
                defaults: new { controller = "Home", action = "Index", page = UrlParameter.Optional }
            );

            // handle non-existing urls
            routes.MapRoute(
                name: "404-PageNotFound",
                url: "{*url}",
                defaults: new { controller = "Home", action = "NotFound" }
            );
        }
    }
}
