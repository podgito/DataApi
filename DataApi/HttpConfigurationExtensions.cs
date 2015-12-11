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
        public static IProvisionalDataApiRoute AddDataApiRoute(this HttpConfiguration config, string routeName, string routeTemplate)
        {
            return new ProvisionalDataApiRoute(routeName, routeTemplate, config);
        }

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

            public DataApiRouteDefinition ToQuery(string query)
            {
                return new DataApiRouteDefinition(this, query);
            }
        }

        public class DataApiRouteDefinition
        {
            private IDictionary<string, object> _defaults;
            private UrlTemplateDefinition _templateDefinition;
            private string _routeName;

            public DataApiRouteDefinition(UrlTemplateDefinition templateDefinition, string query)
            {
                _templateDefinition = templateDefinition;
                _defaults = new Dictionary<string, object>();
                _defaults.Add(RouteDataConstants.ControllerKey, RouteDataConstants.DataApiControllerName);
                _defaults.Add(RouteDataConstants.QueryKey, query);
                _defaults.Add(RouteDataConstants.DataModelKey, new SQLServerClass(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Projects\DataApi\DataApi.Sample\App_Data\SampleDB.mdf;Integrated Security=True"));

                _routeName = Guid.NewGuid().ToString(); //routes need a unique name

                //TODO add querystirng parameters to look for

                templateDefinition.Config.Routes.MapHttpRoute(_routeName, _templateDefinition.RouteTemplate, _defaults);
            }

            public void Returns<T>() where T : new()
            {
                var mapping = new Func<DataTable, T>(dt => dt.MapTo<T>().FirstOrDefault());
                var defaults = _templateDefinition.Config.Routes[_routeName].Defaults;

                defaults.Add(RouteDataConstants.MappingFunctionKey, mapping);
            }

            public void ReturnsArrayOf<T>() where T : new()
            {
                var mapping = new Func<DataTable, IEnumerable<T>>(dt => dt.MapTo<T>());
                var defaults = _templateDefinition.Config.Routes[_routeName].Defaults;

                defaults.Add(RouteDataConstants.MappingFunctionKey, mapping);
            }

            public void Returns<T>(Func<DataTable, T> mappingFunction)
            {
                var defaults = _templateDefinition.Config.Routes[_routeName].Defaults;

                defaults.Add(RouteDataConstants.MappingFunctionKey, mappingFunction);
            }
        }

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

        private class FluentRouteSyntax
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