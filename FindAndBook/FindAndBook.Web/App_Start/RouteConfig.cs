using System.Web.Mvc;
using System.Web.Routing;

namespace FindAndBook.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.LowercaseUrls = true;
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Profile",
                url: "Account/Profile/{username}",
                defaults: new { controller = "Account", action = "Profile" });

            routes.MapRoute(
                name: "Order",
                url: "{controller}/{action}/{id}/{bookingId}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, bookingId = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Logs.Web.Controllers" }
            );
        }
    }
}
