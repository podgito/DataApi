using DataApi.Internals;
using DataApi.Internals.Data.SQLServer;
using DataApi.Internals.Flow;
using DataTableMapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Routing;

namespace DataApi
{
    public static class HttpConfigurationExtensions
    {

        public static IRouteMapping AddDataApiRoute(this HttpConfiguration config, string routeName, string routeTemplate, string query, object defaults = null)
        {
            if (defaults == null) defaults = new { };

            IDictionary<string, object> defaults2 = defaults.ToDictionary();
            defaults2.Add(RouteDataConstants.ControllerKey, RouteDataConstants.DataApiControllerName);
            defaults2.Add(RouteDataConstants.QueryKey, query);

            var route = config.Routes.MapHttpRoute(routeName, routeTemplate, defaults2);

            return new RouteMapping(route);
        }

        public static void MapDataApiRoute(this HttpConfiguration config, string routeTemplate, object defaults = null)
        {
        }

    }
}