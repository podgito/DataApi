using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Routing;

namespace DataApi.Internals.Flow
{
    public interface IRouteMapping
    {
        IHttpRoute Route { get; }

        void Returns<T>() where T : new();

       // void Returns<T, TResult>(Func<IEnumerable<T>, TResult> filterFunction);

        void ReturnsArray<T>() where T : new();

       // void ReturnsArray<T>(Func<IEnumerable<T>, IEnumerable<T>> filterFunction);



    }
}
