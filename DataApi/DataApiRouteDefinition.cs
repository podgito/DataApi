using DataApi.Internals;
using DataTableMapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Http;

namespace DataApi
{
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
            _defaults.Add(RouteDataConstants.DataSourceKey, templateDefinition.DataSource);

            _routeName = Guid.NewGuid().ToString(); //routes need a unique name

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
}