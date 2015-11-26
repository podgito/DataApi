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
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
    }
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

        public object Get(string query)
        {
            //var storedProcedure = this.ControllerContext.RouteData.Values["storedProcedure"].ToString();

            //Get all the inputs
            dynamic inputs =
    ControllerContext.RouteData.Values.Where(kvp => !(kvp.Key == RouteDataConstants.ControllerKey || kvp.Key == RouteDataConstants.QueryKey));


            var conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Projects\DataApi\DataApi.Sample\App_Data\SampleDB.mdf;Integrated Security=True";

            var sql = new SQLServerClass(conn);
            var requiredSqlParameters = _sqlParameterResolver.Resolve(query);

            var sqlParameters = new Dictionary<string, object>();
            foreach(var parameter in requiredSqlParameters)
            {
                sqlParameters.Add(parameter, ControllerContext.RouteData.Values[parameter]);
            }

            var table = sql.ExecuteQuery(query, sqlParameters);

           


            var mapping = (Func<DataTable, object>)this.ControllerContext.RouteData.Values[RouteDataConstants.MappingFunctionKey];
            return mapping(table);

        }

    }
}
