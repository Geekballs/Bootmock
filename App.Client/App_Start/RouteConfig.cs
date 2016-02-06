using System.Web.Mvc;
using System.Web.Routing;

namespace App.Client
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection config)
        {
            config.LowercaseUrls = true;
            config.IgnoreRoute("{resource}.axd/{*pathInfo}");
            config.MapMvcAttributeRoutes();

            config.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}