using DataApi.Internals;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using Shouldly;
using System.Linq;

namespace DataApi.Tests
{
    [TestFixture]
    public class DataApiControllerTests
    {
        private DataApiController controller;
        private HttpConfiguration config;

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

        }

        [Test]
        public void Get_Returns_List_Of_Products()
        {
            //Arrange
            const string query = "SELECT * FROM tblProducts";
            var mockDataSource = new Mock<ISQLDataSource>();

            mockDataSource.Setup(ds => ds.ExecuteQuery(query, It.IsAny<Dictionary<string, object>>())).Returns(CreateDataTable());

            var binding = new QueryBinding(new RouteBinding(config, "api/products/{productId}", mockDataSource.Object), query);
            binding.ReturnsArrayOf<Product>();

            controller.ControllerContext.RouteData.Values[DataApiConstants.QueryBindingKey] = binding;

            //Act
            IEnumerable<Product> responseObject = (IEnumerable<Product>)controller.Get();

            //Assert
            responseObject.ToList().ShouldBeOfType<List<Product>>();
        }

        [Test]
        public void Get_Returns_Single_Product()
        {
            //Arrange
            const string query = "SELECT * FROM tblProducts";
            var mockDataSource = new Mock<ISQLDataSource>();

            mockDataSource.Setup(ds => ds.ExecuteQuery(query, It.IsAny<Dictionary<string, object>>())).Returns(CreateDataTable());

            var binding = new QueryBinding(new RouteBinding(config, "api/products/{productId}", mockDataSource.Object), query);
            binding.Returns<Product>();

            controller.ControllerContext.RouteData.Values[DataApiConstants.QueryBindingKey] = binding;

            //Act
            var responseObject = controller.Get();

            //Assert
            responseObject.ShouldBeOfType<Product>();
        }

        [Test]
        public void Get_Returns_BadRequest400_When_All_SQL_Parameters_Cannot_Be_Resolved()
        {
            const string query = "SELECT * FROM tblProducts WHERE ProductId = @ProductId";
            var binding = new QueryBinding(new RouteBinding(config, "api/products/{productId}", null), query);
            binding.Returns<object>(); //create a fake binding that won't get executed anyway

            controller.ControllerContext.RouteData.Values[DataApiConstants.QueryBindingKey] = binding;
            var exception = Assert.Throws<HttpResponseException>(() => controller.Get());
            Assert.That(exception.Response.StatusCode == System.Net.HttpStatusCode.BadRequest);
        }

        [Test]
        public void Get_Throws_Exception_When_No_Mapping_Function_Exists()
        {
            controller.ControllerContext.RouteData.Values[DataApiConstants.QueryBindingKey] = new QueryBinding(new RouteBinding(config, "api/products/{productId}", null), "");
            var exception = Assert.Throws<HttpResponseException>(() => controller.Get(), DataApiConstants.GetWithoutMappingErrorMessage);
            Assert.That(exception.Response.StatusCode == System.Net.HttpStatusCode.MethodNotAllowed);
        }


        DataTable CreateDataTable()
        {
            var table = new DataTable();

            table.Columns.Add("Name");
            table.Columns.Add("Id", typeof(int));
            table.Rows.Add("Xbox", 1);
            return table;
        }

        class Product
        {
            public string Name { get; set; }
            public int Id { get; set; }
        }
    }
}