using DataApi.Internals;
using DataApi.Internals.Data.SQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DataApi
{
    public class DataApiController : ApiController
    {
        private static SQLParameterResolver _sqlParameterResolver = new SQLParameterResolver();

        public object IdealGet(string storedProcedure, IDictionary<string, object> inputs, Func<DataTable, object> mapping, object dataProvider)
        {
            throw new NotImplementedException();
        }

        public object Get()
        {
            QueryBinding queryBinding = (QueryBinding)ControllerContext.RouteData.Values[DataApiConstants.QueryBindingKey];

            if (queryBinding.MappingFunction == null) Error(HttpStatusCode.MethodNotAllowed, DataApiConstants.GetWithoutMappingErrorMessage);


            var query = queryBinding.Query;

            var requiredSqlParameters = _sqlParameterResolver.Resolve(query);

            var sqlParameters = new Dictionary<string, object>();
            foreach (var parameter in requiredSqlParameters)
            {
                if (!ControllerContext.RouteData.Values.ContainsKey(parameter)) Error(HttpStatusCode.BadRequest, "Parameter missing");
                sqlParameters.Add(parameter, ControllerContext.RouteData.Values[parameter]);
            }

            ISQLDataSource dataModel = queryBinding.RouteBinding.DataSource;
            var table = dataModel.ExecuteQuery(query, sqlParameters);

            var mapping = queryBinding.MappingFunction;
            return mapping(table);
        }

        void Error(HttpStatusCode statusCode, string errorMessage)
        {
            var response = new HttpResponseMessage
            {
                StatusCode  = statusCode,
                Content = new StringContent(errorMessage)
            };
            throw new HttpResponseException(response);
        }
    }
}