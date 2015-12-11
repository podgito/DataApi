using System.Collections.Generic;
using System.Data;

namespace DataApi.Internals.Data.SQLServer
{
    public interface ISQLDataModel
    {
        DataTable ExecuteQuery(string query);
        DataTable ExecuteQuery(string query, Dictionary<string, object> inputParameters);
    }
}