using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataApi.DataProviders
{
    public interface IDataProvider
    {

        void ExecuteStoredProcedure(string storedProcedure, IDictionary<string, object> inputs);
        DataTable GetDataTable(string storedProcedure, IDictionary<string, object> inputs);

    }
}
