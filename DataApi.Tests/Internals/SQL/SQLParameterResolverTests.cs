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
        public void SQL_WITH_NO_INPUT_PARAMETERS_YIELDS_NO_RESULTS()
        {
            var sql = "SELECT * FROM Employees";

            var parameters = _resolver.Resolve(sql);

            parameters.Count().ShouldBe(0);
        }

        [Test]
        public void SQL_WITH_PARAMETER_USED_MULTIPLE_TIMES_ONLY_RETURNED_ONCE()
        {
            var sql = "SELECT *, @ProductId FROM Employees WHERE ProductId = @ProductId ";

            var parameters = _resolver.Resolve(sql);

            parameters.Single().ShouldBe("ProductId");
        }
    }
}