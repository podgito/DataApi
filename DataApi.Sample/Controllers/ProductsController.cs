using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DataApi.Sample.Controllers
{
    public class ProductsController : ApiController
    {

        public IEnumerable<object> Get()
        {
            return Enumerable.Empty<object>();
        }

        public object Get(int productId)
        {
            return new { productId = productId };
        }
    }
}
