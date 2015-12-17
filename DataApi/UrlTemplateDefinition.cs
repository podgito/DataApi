using System;
using System.Linq;
using System.Collections.Generic;
using DataApi.Internals;
using System.Web.Http;

namespace DataApi
{
    public class UrlTemplateDefinition
    {
        internal string RouteTemplate { get; private set; }
        internal string[] QueryStringParameters { get; private set; }
        internal HttpConfiguration Config { get; private set; }
        internal ISQLDataSource DataSource;

        public UrlTemplateDefinition(HttpConfiguration config, string routeTemplate, ISQLDataSource dataSource)
        {
            Config = config;
            RouteTemplate = routeTemplate;
            this.DataSource = dataSource;
        }

        //public UrlTemplateDefinition WithQueryStringParameters(params string[] queryStringParameters)
        //{
        //    QueryStringParameters = queryStringParameters;
        //    return this;
        //}

        public DataApiRouteDefinition ToQuery(string query)
        {
            return new DataApiRouteDefinition(this, query);
        }
    }
}