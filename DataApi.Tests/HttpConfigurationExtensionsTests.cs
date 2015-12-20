//using NUnit.Framework;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web.Http;
//using DataApi;

//namespace DataApi.Tests
//{
//    [TestFixture]
//    public class HttpConfigurationExtensionsTests
//    {
//        [Test]
//        public void AddStoredProcedureRouteCreateHttpMapRoute()
//        {
//            HttpConfiguration config = new HttpConfiguration();

//            var routeName = "UnitTestRoute";
//            var routeTemplate = "api/products/{productId}";
//            var storedProcedureName = "USP_GetProducts";
//            var defaults = new { productId = 0 };

//            //Act
//            config.AddDataApiRoute(routeName, routeTemplate, storedProcedureName, defaults);

//            //Assert
//            var routes = config.Routes;
//            var r = routes.First();

//            Assert.AreEqual(1, routes.Count);

//            Assert.AreEqual("DataApi", r.Defaults["controller"]);
//            Assert.AreEqual(routeTemplate, r.RouteTemplate);

//        }


//    }
//}
