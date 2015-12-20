using DataApi.Internals;
using System.Web.Http;

namespace DataApi
{
    public static class DataApiExtensions
    {
        /// <summary>
        /// Create a binding with SQLDataSource to create DataApi endpoints
        /// </summary>
        /// <param name="config"></param>
        /// <param name="dataProvider"></param>
        /// <returns></returns>
        public static DataSourceBinding CreateDataApiBinding(this HttpConfiguration config, ISQLDataSource dataProvider)
        {
            return new DataSourceBinding(config, dataProvider);
        }
    }
}