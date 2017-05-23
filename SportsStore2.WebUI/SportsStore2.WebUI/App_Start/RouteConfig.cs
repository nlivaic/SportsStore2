using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SportsStore2.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "",
                url: "Page{page}",
                defaults: new { controller = "Product", action = "List", category = (string)null, id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "",
                url: "{category}",
                defaults: new { controller = "Product", action = "List", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "",
                url: "{category}/Page{page}",
                defaults: new { controller = "Product", action = "List", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default",
                url: "",
                defaults: new { controller = "Product", action = "List", category = (string)null, page = 1, id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "",
                url: "{controller}/{action}",
                defaults: new { id = UrlParameter.Optional }
            );
        }
    }
}
