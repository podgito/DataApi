using DataApi.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace DataApi.Internals.Flow
{
    public interface IProvisionalDataApiRoute
    {
        HttpConfiguration Configuration { get; }
        string RouteName { get; }
        string RouteTemplate { get; }

        IRouteMapping MapToStoredProcedure<TProvider>(string storedProcedure) where TProvider : IDataProvider, new();

    }
}
