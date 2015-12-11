using System.Web.Http;

namespace DataApi.Sample
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.MapDataApiRoute("api/products")
                .ToQuery("SELECT * FROM Products") //Only then is the route created since we have a SP to call
                .ReturnsArrayOf<Product>(); //Add the return type

            config.MapDataApiRoute("api/products/{productId}")
                .WithQueryStringParameters("productId")
                .ToQuery("SELECT * FROM Products WHERE ProductId = @ProductId") //Only then is the route created since we have a SP to call
                .Returns<Product>(); //Add the return type

            //Old
            //config.AddDataApiRoute("Sales", "api/sales/{saleId}", "USP_GetProducts");

            //config.AddDataApiRoute("Sales2", "api/v2/sales").MapToStoredProcedure<SqlServerDataProvider>("USP_GetSales").ReturnsArray<Product>();

            //config.AddDataApiRoute("Sales2", "api/v2/sales").MapToStoredProcedure<SqlServer>("USP_GetSales").ReturnsArrayOf<ProductSale>();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}