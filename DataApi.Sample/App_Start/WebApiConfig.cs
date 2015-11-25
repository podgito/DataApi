using DataApi.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq;
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

            //var x = typeof(DataApi.);

            //config.Routes.MapHttpRoute("Products", "api/products/{productId}", defaults: new { controller = "DataApi", productId = RouteParameter.Optional });

            //config.AddDataApiRoute("ProductsApi", "api/products", "USP_GetProducts").ReturnsArray<Product>();
            //config.AddDataApiRoute("ProductApi", "api/products/{productId}", "USP_GetProducts").Returns<Product>();

            //Config.AppDataApiRoute("RouteName", "api/products").ToStoredProcedure("GetProducts"). 

            config.MapDataApiRoute("api/products")
                .WithQueryStringParameters("date", "sku")
                .ToQuery("SELECT * FROM Products") //Only then is the route created since we have a SP to call
                .ReturnsEnumerableOf<Product>(); //Add the return type

            config.MapDataApiRoute("api/products/{productId}")
                .WithQueryStringParameters("date", "sku")
                .ToQuery("SELECT * FROM Products") //Only then is the route created since we have a SP to call
                .Returns<Product>(); //Add the return type

            config.AddDataApiRoute("Sales", "api/sales/{saleId}", "USP_GetProducts");

            config.AddDataApiRoute("Sales2", "api/v2/sales").MapToStoredProcedure<SqlServerDataProvider>("USP_GetSales").ReturnsArray<Product>();

            //config.AddDataApiRoute("Sales2", "api/v2/sales").MapToStoredProcedure<SqlServer>("USP_GetSales").ReturnsArrayOf<ProductSale>();

            

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            
        }
    }
}
