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

            config.AddDataApiRoute("ProductsApi", "api/products", "USP_GetProducts").ReturnsArray<Product>();
            config.AddDataApiRoute("ProductApi", "api/products/{productId}", "USP_GetProducts").Returns<Product>();

            config.AddDataApiRoute("Sales", "api/sales/{saleId}", "USP_GetProducts");

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
