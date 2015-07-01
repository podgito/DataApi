using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using DataApi.Internals;
using System.Data;
using DataApi.Internals.Flow;
using DataTableMapper;
using System.Web.Http.Routing;

namespace DataApi
{
    public static class HttpConfigurationExtensions
    {

        public static IProvisionalDataApiRoute AddDataApiRoute(this HttpConfiguration config, string routeName, string routeTemplate)
        {
            return new ProvisionalDataApiRoute(routeName, routeTemplate, config);
        }

        public static IRouteMapping AddDataApiRoute(this HttpConfiguration config, string routeName, string routeTemplate, string storedProcedureName, object defaults = null)
        {
            if (defaults == null) defaults = new { };

            IDictionary<string, object> defaults2 = defaults.ToDictionary();
            defaults2.Add("controller", "DataApi");
            defaults2.Add("storedProcedure", storedProcedureName);
            
            var route = config.Routes.MapHttpRoute(routeName, routeTemplate, defaults2);

            return new RouteMapping(route);
        }

        public static void MapDataApiRoute(this HttpConfiguration config, string routeTemplate, object defaults = null)
        {

        }

        class FluentRouteSyntax
        {

            public FluentRouteSyntax(string routeName, IHttpRoute route)
            {
                RouteName = routeName;
                Route = Route;
            }

            internal string RouteName { get; private set; }
            internal IHttpRoute Route { get; private set; }
            
        }

    }
}
