using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Shouldly;
using System.Web.Http;
using Moq;
using DataApi.Internals;
using System.Net.Http;
using System.Web.Http.Routing;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;

namespace DataApi.Tests
{
    [TestFixture]
    public class DataApiControllerTests
    {
        private DataApiController controller;
        HttpConfiguration config;

        [SetUp]
        public void Setup()
        {
            config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/products");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "products" } });

            controller = new DataApiController();
            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;

            controller.ControllerContext.RouteData.Values[RouteDataConstants.DataSourceKey] = new Mock<ISQLDataSource>().Object;
            
        }

        [Test]
        public void Get_Returns_BadRequest400_When_All_SQL_Parameters_Cannot_Be_Resolved()
        {
            const string query = "SELECT * FROM tblProducts WHERE ProductId = @ProductId";
            controller.ControllerContext.RouteData.Values[RouteDataConstants.QueryBindingKey] = new QueryBinding(new RouteBinding(config, "api/products/{productId}", null), query);
            var exception = Assert.Throws<HttpResponseException>(() => controller.Get());
            Assert.That(exception.Response.StatusCode == System.Net.HttpStatusCode.BadRequest);
        }

    }
}
