using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using DataApi.Internals;

namespace DataApi
{
    public static class HttpConfigurationExtensions
    {

        public static object AddDataApiRoute(this HttpConfiguration config, string routeName, string routeTemplate, string storedProcedureName, object defaults = null)
        {

            if (defaults == null) defaults = new { controller = "DataApi" };

            else defaults = defaults.AddProperty("controller", "DataApi");
            
            var route = config.Routes.MapHttpRoute(routeName, routeTemplate, defaults);
            return route;
        }

    }
}
