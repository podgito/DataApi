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
        private DataApiController _controller;

        [SetUp]
        public void Setup()
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/products");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "products" } });

            _controller = new DataApiController();
            _controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            _controller.Request = request;
            _controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;

            _controller.ControllerContext.RouteData.Values[RouteDataConstants.DataSourceKey] = new Mock<ISQLDataSource>().Object;
        }

        [Test]
        public void Get_Returns_BadRequest400_When_All_SQL_Parameters_Cannot_Be_Resolved()
        {
            var exception = Assert.Throws<HttpResponseException>(() => _controller.Get("SELECT * FROM tblProducts WHERE ProductId = @ProductId"));
            Assert.That(exception.Response.StatusCode == System.Net.HttpStatusCode.BadRequest);
        }

    }
}
