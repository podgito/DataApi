using DataApi.Internals;
using DataTableMapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Http;

namespace DataApi
{
    //TODO: Needs to be more testable - internal state needs to be examined OR delegate this logic to another class
    public class QueryBinding
    {
        public RouteBinding RouteBinding { get; set; }
        private string _routeName;
        public string Query { get; set; }
        public Func<DataTable, object> MappingFunction { get; private set; }

        public QueryBinding(RouteBinding routeBinding, string query)
        {
            RouteBinding = routeBinding;
            Query = query;

            var defaults = new Dictionary<string, object>();
            defaults.Add(DataApiConstants.ControllerKey, DataApiConstants.DataApiControllerName);
            defaults.Add(DataApiConstants.QueryBindingKey, this);

            _routeName = Guid.NewGuid().ToString(); //routes need a unique name

            routeBinding.Config.Routes.MapHttpRoute(_routeName, RouteBinding.RouteTemplate, defaults);
        }

        public void Returns<T>() where T : new()
        {
            Func<DataTable, object> mapping = dt => dt.MapTo<T>().FirstOrDefault();

            Returns(mapping);
        }

        public void ReturnsArrayOf<T>() where T : new()
        {
            Func<DataTable, IEnumerable<T>> mapping = dt => dt.MapTo<T>();

            Returns(mapping);
        }

        public void Returns(Func<DataTable, object> mappingFunction)
        {
            MappingFunction = mappingFunction;
        }
    }
}