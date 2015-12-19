using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using DataApi.Internals;
using System.Data;
using DataTableMapper;
using DataApi.Internals.Data.SQLServer;
using DataApi.Internals.Data.SQL;

namespace DataApi
{
    
    public class DataApiController : ApiController
    {

        static SQLParameterResolver _sqlParameterResolver = new SQLParameterResolver();

        

        //public DataApiController(Func<DataTable, object> mappingFunction, string storedProcedure)
        //{

        //}

        public object IdealGet(string storedProcedure, IDictionary<string, object> inputs, Func<DataTable, object> mapping, object dataProvider)
        {
            throw new NotImplementedException();
        }

        public object Get()
        {

            QueryBinding queryBinding = (QueryBinding)ControllerContext.RouteData.Values[RouteDataConstants.QueryBindingKey];

            ISQLDataSource dataModel = queryBinding.RouteBinding.DataSource; // (ISQLDataSource)ControllerContext.RouteData.Values[RouteDataConstants.DataSourceKey];

            var query = queryBinding.Query;

            var requiredSqlParameters = _sqlParameterResolver.Resolve(query);

            var sqlParameters = new Dictionary<string, object>();
            foreach(var parameter in requiredSqlParameters)
            {
                if (!ControllerContext.RouteData.Values.ContainsKey(parameter)) throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest);
                sqlParameters.Add(parameter, ControllerContext.RouteData.Values[parameter]);
            }

            var table = dataModel.ExecuteQuery(query, sqlParameters);

            var mapping = queryBinding.MappingFunction;
            return mapping(table);

        }

    }
}
