using DataApi.Internals;
using System;
using System.Web.Http;

namespace DataApi
{
    public class RouteBinding
    {
        internal string RouteTemplate { get; private set; }

        //internal string[] QueryStringParameters { get; private set; }
        internal HttpConfiguration Config { get; private set; }

        internal ISQLDataSource DataSource { get; private set; }

        public RouteBinding(HttpConfiguration config, string routeTemplate, ISQLDataSource dataSource)
        {
            Config = config;
            RouteTemplate = routeTemplate;
            DataSource = dataSource;
        }

        //public RouteBinding WithQueryStringParameters(params string[] queryStringParameters)
        //{
        //    QueryStringParameters = queryStringParameters;
        //    return this;
        //}

        public QueryBinding ToQuery(string query)
        {
            return new QueryBinding(this, query);
        }

        public QueryBinding ToStoredProcedure(string storedProcedureName)
        {
            throw new NotImplementedException();
        }
    }
}