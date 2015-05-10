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

            config.Routes.MapHttpRoute("Products", "api/products/{productId}", defaults: new { controller = "DataApi", productId = RouteParameter.Optional });
            
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
