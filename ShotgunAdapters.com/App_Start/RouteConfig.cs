using Cstieg.ControllerHelper;
using ShotgunAdapters.Controllers;
using ShotgunAdapters.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShotgunAdapters
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "ProductsByCaliber",
                url: "{*caliberName}",
                defaults: new { controller = "Home", action = "ProductsByCaliber" },
                constraints: new { caliberName = new IsCaliberNameConstraint() }
            );

            routes.MapRoute(
                name: "Home",
                url: "{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                constraints: new { action = new IsHomeActionConstraint() }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }

    class IsCaliberNameConstraint : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            var db = new ApplicationDbContext();
            if (values[parameterName] != null)
            {
                var caliberName = values[parameterName].ToString();
                return db.Calibers.Any(c => c.Name.ToLower() == caliberName.ToLower());
            }
            return false;
        }
    }

    class IsHomeActionConstraint : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return ControllerHelper.HasAction(typeof(HomeController), values[parameterName].ToString());
        }
    }
}
