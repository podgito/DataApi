using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Routing;
using DataTableMapper;

namespace DataApi.Internals.Flow
{
    class RouteMapping : IRouteMapping
    {

        public RouteMapping(IHttpRoute route)
        {
            Route = route;
        }


        public System.Web.Http.Routing.IHttpRoute Route { get; private set; }

        public void Returns<T>() where T : new()
        {
            var mapping = new Func<DataTable, T>(dt => dt.MapTo<T>().FirstOrDefault());
            var defaults = Route.Defaults;

            defaults.Remove("mapping");
            defaults.Add("mapping", mapping);


            //Route

            //Route.Defaults.AddProperty("mapping", mapping);

        }

        public void ReturnsArray<T>() where T : new()
        {
            var mapping = new Func<DataTable, IEnumerable<T>>(dt => dt.MapTo<T>());
            var defaults = Route.Defaults;
            defaults.Remove("mapping");
            defaults.Add("mapping", mapping);
        }
    }
}
