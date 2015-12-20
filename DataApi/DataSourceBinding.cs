using System.Web.Http;
using DataApi.Internals;

namespace DataApi
{
    /// <summary>
    /// Holds data source binding for mapping DataAp endpoints
    /// </summary>
    public class DataSourceBinding
    {
        private HttpConfiguration config;
        private ISQLDataSource dataSource;

        //Marked internal because we don't want devs creating multiple instances
        internal DataSourceBinding(HttpConfiguration config, ISQLDataSource dataSource)
        {
            this.config = config;
            this.dataSource = dataSource;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="routeTemplate"></param>
        /// <returns></returns>
        public RouteBinding MapDataApiRoute(string routeTemplate)
        {
            return new RouteBinding(config, routeTemplate, dataSource);
        }

    }
}