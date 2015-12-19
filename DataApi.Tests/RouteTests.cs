using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataApi;
using MvcRouteTester;

namespace DataApi.Tests
{
    [TestFixture]
    public class RouteTests
    {

        System.Web.Http.HttpConfiguration _config;

        [SetUp]
        public void Setup()
        {
            //GlobalConfiguration.Configure(WebApiConfig.Register);

            _config = new System.Web.Http.HttpConfiguration();

            _config.EnsureInitialized();
        }


        [Test]
        [TestCase("api/products/", "/api/products")]
        [TestCase("api/products/{productId}", "/api/products/123")]
        [TestCase("api/products/{productId}", "/api/products/123?isNew=true")]
        public void TestDataApiRouteMapping(string routeTemplate, string route)
        {

            var storedProcedure = "USP_UnitTest";
            _config.AddDataApiRoute("bla", routeTemplate, storedProcedure);

            _config.ShouldMap(route).To<DataApiController>(System.Net.Http.HttpMethod.Get, x => x.Get());
        }
    }
}
