using System.Web.Http;
using DataApi.Internals;

namespace DataApi
{
    public class DataSourceBinding
    {
        private HttpConfiguration config;
        private ISQLDataSource dataSource;

        internal DataSourceBinding(HttpConfiguration config, ISQLDataSource dataProvider)
        {
            this.config = config;
            this.dataSource = dataProvider;
        }

        public UrlTemplateDefinition MapDataApiRoute(string routeTemplate)
        {
            return new UrlTemplateDefinition(config, routeTemplate, dataSource);
        }

    }
}