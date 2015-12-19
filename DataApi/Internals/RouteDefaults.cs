using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace DataApi.Internals
{
    /// <summary>
    /// Class to manage the route defaults
    /// </summary>
    internal class RouteDefaults : RouteValueDictionary
    {




        public RouteDefaults(string query, ISQLDataSource dataSource)
        {
            Add(RouteDataConstants.ControllerKey, RouteDataConstants.DataApiControllerName);
            Add(RouteDataConstants.QueryKey, query);
            Add(RouteDataConstants.DataSourceKey, dataSource);
        }





    }


}
