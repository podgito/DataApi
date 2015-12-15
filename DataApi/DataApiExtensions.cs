using DataApi.Internals;
using System.Web.Http;

namespace DataApi
{
    public static class DataApiExtensions
    {
        public static DataSourceBinding CreateDataApiBinding(this HttpConfiguration config, ISQLDataSource dataProvider)
        {
            return new DataSourceBinding(config, dataProvider);
        }
    }
}