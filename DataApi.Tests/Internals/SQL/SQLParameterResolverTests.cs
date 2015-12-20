using DataApi.Internals.Data.SQL;
using NUnit.Framework;
using Shouldly;
using System.Linq;

namespace DataApi.Tests.Internals.SQL
{
    [TestFixture]
    public class SQLParameterResolverTests
    {
        private SQLParameterResolver _resolver;

        [SetUp]
        public void Setup()
        {
            _resolver = new SQLParameterResolver();
        }

        [Test]
        public void Sql_With_No_Input_Parameters_Yields_No_Results()
        {
            var sql = "SELECT * FROM Employees";

            var parameters = _resolver.Resolve(sql);

            parameters.Count().ShouldBe(0);
        }

        [Test]
        public void Sql_With_Two_Different_Parameters_Yields_One_Each()
        {
            
            var sql = "SELECT * FROM Employees WHERE ProductId = @ProductId AND Name = @Name";
            
            var parameters = _resolver.Resolve(sql);

            parameters.Count().ShouldBe(2);
            parameters.ShouldContain("ProductId");
            parameters.ShouldContain("Name");
        }

        [Test]
        public void Sql_With_Parameter_Used_Multiple_Times_Only_Returned_Once()
        {
            var sql = "SELECT *, @ProductId FROM Employees WHERE ProductId = @ProductId ";

            var parameters = _resolver.Resolve(sql);

            parameters.Single().ShouldBe("ProductId");
        }
    }
}