using DataApi.Internals.Data.SQLServer;
using MvcRouteTester;
using NUnit.Framework;
using System.Net.Http;

namespace DataApi.Tests
{
    [TestFixture]
    public class RouteTests
    {
        private System.Web.Http.HttpConfiguration config;

        [SetUp]
        public void Setup()
        {
            //GlobalConfiguration.Configure(WebApiConfig.Register);

            config = new System.Web.Http.HttpConfiguration();

            config.EnsureInitialized();
        }

        [Test]
        [TestCase("api/products/", "/api/products")]
        [TestCase("api/products/{productId}", "/api/products/123")]
        [TestCase("api/products/{productId}", "/api/products/123?isNew=true")]
        public void TestDataApiRouteMapping(string routeTemplate, string route)
        {
            var dataApiBinding = config.CreateDataApiBinding(new SQLServerDataSource(""));

            dataApiBinding.MapDataApiRoute(routeTemplate)
                .ToQuery("SELECT * FROM Products");

            config.ShouldMap(route).To<DataApiController>(HttpMethod.Get, x => x.Get());
        }
    }
}