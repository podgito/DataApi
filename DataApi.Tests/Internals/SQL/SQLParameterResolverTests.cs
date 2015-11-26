using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using DataApi.Internals.Data.SQL;

namespace DataApi.Tests.Internals.SQL
{

    [TestFixture]
    public class SQLParameterResolverTests
    {
        SQLParameterResolver _resolver;
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
