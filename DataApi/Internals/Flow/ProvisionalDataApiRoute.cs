using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace DataApi.Internals.Flow
{
    public class ProvisionalDataApiRoute : IProvisionalDataApiRoute
    {

        public ProvisionalDataApiRoute(string name, string routeTemplate, HttpConfiguration config)
        {
            RouteName = name;
            RouteTemplate = routeTemplate;
            Configuration = config;
        }

        public string RouteName
        {
            get;
            protected set;
        }

        public string RouteTemplate
        {
            get;
            protected set;
        }

        public System.Web.Http.HttpConfiguration Configuration
        {
            get;
            protected set;
        }


        public IRouteMapping MapToStoredProcedure<TProvider>(string storedProcedure) where TProvider : DataProviders.IDataProvider, new()
        {
            var dataProvider = new TProvider();

            IDictionary<string, object> defaults = new Dictionary<string, object>();
            defaults.Add("controller", "DataApi");
            defaults.Add("storedProcedure", storedProcedure);
            defaults.Add("dataProvider", dataProvider);
            defaults.Add("mapping", new Func<DataTable, object>(dt => new { }));

            var route = this.Configuration.Routes.MapHttpRoute(this.RouteName, this.RouteTemplate, defaults);

            return new RouteMapping(route);

        }

        
    }
}
