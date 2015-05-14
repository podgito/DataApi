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

namespace DataApi
{
    public static class HttpConfigurationExtensions
    {


        public static IRouteMapping AddDataApiRoute(this HttpConfiguration config, string routeName, string routeTemplate, string storedProcedureName, object defaults = null)
        {
            if (defaults == null) defaults = new { };

            IDictionary<string, object> defaults2 = defaults.ToDictionary();
            defaults2.Add("controller", "DataApi");
            defaults2.Add("storedProcedure", storedProcedureName);
            
            var route = config.Routes.MapHttpRoute(routeName, routeTemplate, defaults2);

            return new RouteMapping(route);
        }

    }
}
