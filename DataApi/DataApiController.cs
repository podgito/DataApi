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

namespace DataApi
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
    }
    public class DataApiController : ApiController
    {


        //public DataApiController(Func<DataTable, object> mappingFunction, string storedProcedure)
        //{

        //}

        private DataTable CreateProductsTable()
        {
            var table = new DataTable();
            table.Columns.Add("ProductId");
            table.Columns.Add("Name");
            return table;
        }

        public object Get(string storedProcedure)
        {
            //var storedProcedure = this.ControllerContext.RouteData.Values["storedProcedure"].ToString();

            //Get all the inputs
            dynamic inputs =
    ControllerContext.RouteData.Values.Where(
        kvp => !(kvp.Key == "controller" || kvp.Key == "storedProcedure"));

            //Map to dictionary<string, object>

            //Call stored procedure

            var conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Projects\DataApi\DataApi.Sample\App_Data\SampleDB.mdf;Integrated Security=True";

            var sql = new SQLServerClass(conn);

            var table = sql.ExecuteQuery(storedProcedure);

            //var table = CreateProductsTable();
            //table.Rows.Add(1, "Xbox");
            //table.Rows.Add(2, "PS3");

            //table.MapTo<Product>();

            //Map result using mapping function


            var mapping = (Func<DataTable, object>)this.ControllerContext.RouteData.Values["mapping"];
            return mapping(table);

            //this.ControllerContext.RequestContext.RouteData.Route.


            //return new
            //{
            //    storedProcedure,
            //    inputs = inputs,
            //    routeData = this.ControllerContext.RouteData.Values
            //};
        }

    }
}
