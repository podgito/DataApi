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
using Newtonsoft.Json.Serialization;
using DataTableMapper;

namespace DataApi
{
    public static class HttpConfigurationExtensions
    {


        public static UrlTemplateDefinition MapDataApiRoute(this HttpConfiguration config, string routeTemplate)
        {
            return new UrlTemplateDefinition(config, routeTemplate);
        }

        public class UrlTemplateDefinition
        {
            public string RouteTemplate { get; private set; }
            public string[] QueryStringParameters { get; private set; }
            public HttpConfiguration Config { get; private set; }

            public UrlTemplateDefinition(HttpConfiguration config, string routeTemplate)
            {
                Config = config;
                RouteTemplate = routeTemplate;
            }

            public UrlTemplateDefinition WithQueryStringParameters(params string[] queryStringParameters)
            {
                QueryStringParameters = queryStringParameters;
                return this;
            }

            public DataApiRouteDefinition ToStoredProcedure(string storedProcedureName)
            {
                return new DataApiRouteDefinition(this, storedProcedureName);
            }
        }

        public class DataApiRouteDefinition
        {

            private IDictionary<string, object> _defaults;
            private UrlTemplateDefinition _templateDefinition;
            private string _routeName;

            public DataApiRouteDefinition(UrlTemplateDefinition templateDefinition, string storedProcedureName)
            {
                _templateDefinition = templateDefinition;
                _defaults = new Dictionary<string, object>();
                _defaults.Add("controller", "DataApi");
                _defaults.Add("storedProcedure", storedProcedureName);

                _routeName = new Guid().ToString();

                //TODO add querystirng parameters to look for

                templateDefinition.Config.Routes.MapHttpRoute(_routeName, _templateDefinition.RouteTemplate, _defaults);
            }

            public void Returns<T>() where T : new()
            {
                var mapping = new Func<DataTable, T>(dt => dt.MapTo<T>().FirstOrDefault());
                var defaults = _templateDefinition.Config.Routes[_routeName].Defaults;

                defaults.Add("mapping", mapping);
            }


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



    }
}
