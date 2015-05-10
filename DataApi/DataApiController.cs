using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using DataApi.Internals;

namespace DataApi
{
    public class DataApiController : ApiController
    {

        public object Get()
        {
            var storedProcedure = this.ControllerContext.RouteData.Values["storedProcedure"].ToString();

            dynamic inputs =
                ControllerContext.RouteData.Values.Where(
                    kvp => !(kvp.Key == "controller" || kvp.Key == "storedProcedure"));

            return new
            {
                storedProcedure,
                inputs = inputs,
                routeData = this.ControllerContext.RouteData.Values
            };
        }

    }
}
